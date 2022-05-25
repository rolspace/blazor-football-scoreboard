using System;
using Football.Core.Models;
using Football.Core.Persistence.Entities;

namespace Football.Core.Persistence.MySql.Utilities
{
    public class EntityMapper
    {
        public static Game MapToGameModel(GameEntity gameEntity)
        {
            if (gameEntity == null) return null;

            var game = new Game
            {
                Id = gameEntity.Id,
                Week = gameEntity.Week,
                HomeTeam = gameEntity.HomeTeam,
                AwayTeam = gameEntity.AwayTeam,
            };

            return game;
        }

        public static Stat MapToStatModel(StatEntity statEntity)
        {
            if (statEntity == null) return null;

            var stat = new Stat
            {
                GameId = statEntity.GameId,
                Team = statEntity.Team,
                Score = statEntity.Score,
                Quarter = statEntity.Game.Time.Quarter,
                QuarterSecondsRemaining = statEntity.Game.Time.QuarterSecondsRemaining,
                AirYards = statEntity.AirYards,
                Sacks = statEntity.Sacks,
                Punts = statEntity.Punts,
                ReturnYards = statEntity.ReturnYards
            };

            return stat;
        }

        public static Play MapToPlayModel(PlayEntity playEntity)
        {
            if (playEntity == null) return null;

            bool isHomeTeamPossession = playEntity.Posteam == playEntity.HomeTeam;
            bool isAwayTeamPossession = playEntity.Posteam == playEntity.AwayTeam;

            var play = new Play
            {
                Id = playEntity.Id,
                HomeScore = playEntity.TotalHomeScore ?? 0,
                AwayScore = playEntity.TotalAwayScore ?? 0,
                Description = playEntity.Desc,
                Quarter = playEntity.Qtr,
                QuarterSecondsRemaining = playEntity.QuarterSecondsRemaining ?? 0,
                Game = new Game()
                {
                    Id = playEntity.GameId,
                    HomeTeam = playEntity.HomeTeam,
                    AwayTeam = playEntity.AwayTeam,
                    Week = playEntity.Week,
                },
                HomePlayLog = MapToPlayLog(playEntity, playEntity.TotalHomeScore ?? 0, isHomeTeamPossession, playEntity.IsHomeTeamReceivingKickoffOrPunt, playEntity.IsHomeTeamPunting),
                AwayPlayLog = MapToPlayLog(playEntity, playEntity.TotalAwayScore ?? 0, isAwayTeamPossession, playEntity.IsAwayTeamReceivingKickoffOrPunt, playEntity.IsAwayTeamPunting)
            };

            return play;
        }

        private static PlayLog MapToPlayLog(PlayEntity playEntity, int score, bool isInPossession, bool isReceivingKickoffOrPunt, bool isPunting)
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
