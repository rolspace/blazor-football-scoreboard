using System;
using System.Collections.Generic;
using System.Linq;
using Football.Core.Models;

namespace Football.Core.Persistence.Entities
{
    public static class Extensions
    {
        public static Game ToModel(this GameEntity gameEntity)
        {
            if (gameEntity is null) return null;

            return new Game
            {
                Id = gameEntity.Id,
                Week = gameEntity.Week,
                HomeTeam = gameEntity.HomeTeam,
                AwayTeam = gameEntity.AwayTeam,
                Time = gameEntity.Time.ToModel(),
                Stats = gameEntity.Stats.Select(s => s.ToModel()).ToList().AsReadOnly()
            };
        }

        public static Time ToModel(this TimeEntity timeEntity)
        {
            if (timeEntity is null) return null;

            return new Time
            {
                Quarter = timeEntity.Quarter,
                QuarterSecondsRemaining = timeEntity.QuarterSecondsRemaining
            };
        }

        public static Stat ToModel(this StatEntity statEntity)
        {
            if (statEntity == null) return null;

            return new Stat
            {
                GameId = statEntity.GameId,
                Team = statEntity.Team,
                Score = statEntity.Score,
                AirYards = statEntity.AirYards,
                Sacks = statEntity.Sacks,
                Punts = statEntity.Punts,
                ReturnYards = statEntity.ReturnYards
            };
        }

        public static Play ToModel(this PlayEntity playEntity)
        {
            if (playEntity == null) return null;

            bool isHomeTeamPossession = playEntity.Posteam == playEntity.HomeTeam;
            bool isAwayTeamPossession = playEntity.Posteam == playEntity.AwayTeam;

            return new Play
            {
                Id = playEntity.Id,
                HomeScore = playEntity.TotalHomeScore ?? 0,
                AwayScore = playEntity.TotalAwayScore ?? 0,
                Description = playEntity.Desc,
                Quarter = playEntity.Qtr,
                QuarterSecondsRemaining = playEntity.QuarterSecondsRemaining ?? 0,
                Game = playEntity.Game.ToModel(),
                HomePlayLog = playEntity.ToModel(playEntity.TotalHomeScore ?? 0, isHomeTeamPossession, playEntity.IsHomeTeamReceivingKickoffOrPunt, playEntity.IsHomeTeamPunting),
                AwayPlayLog = playEntity.ToModel(playEntity.TotalAwayScore ?? 0, isAwayTeamPossession, playEntity.IsAwayTeamReceivingKickoffOrPunt, playEntity.IsAwayTeamPunting)
            };
        }

        public static PlayLog ToModel(this PlayEntity playEntity, int score, bool isInPossession, bool isReceivingKickoffOrPunt, bool isPunting)
        {
            bool isSpecialTeamsPlay = playEntity.PlayType == "kickoff" || playEntity.PlayType == "punt";

            return new PlayLog()
            {
                Score = score,
                Quarter = playEntity.Qtr,
                QuarterSecondsRemaining = playEntity.QuarterSecondsRemaining ?? 0,
                OffensePlayLog = isInPossession && !isSpecialTeamsPlay ? new OffensePlayLog()
                {
                    AirYards = playEntity.AirYards ?? 0
                }
                :
                null,
                DefensePlayLog = !isInPossession && !isSpecialTeamsPlay ? new DefensePlayLog()
                {
                    Sacks = Convert.ToBoolean(playEntity.Sack) ? 1 : 0
                }
                :
                null,
                SpecialPlayLog = isSpecialTeamsPlay ? new SpecialPlayLog()
                {
                    ReturnYards = isReceivingKickoffOrPunt ? playEntity.ReturnYards ?? 0 : 0,
                    Punts = isPunting && Convert.ToBoolean(playEntity.PuntAttempt) ? 1 : 0
                }
                :
                null
            };
        }
    }
}
