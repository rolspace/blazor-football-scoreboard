using Football.Core.Models;
using Football.Core.Persistence.MySql.Entities;
using System;

namespace Football.Core.Persistence.MySql.Utilities
{
    public class ModelMapper
    {
        public static Game MapGameModel(GameEntity gameEntity)
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

        public static Play MapPlayModel(PlayEntity playEntity)
        {
            if (playEntity == null) return null;

            bool isOffensivePlay = playEntity.IsHomeTeamOnOffense || playEntity.IsAwayTeamOnOffense;
            bool isSpecialTeamsPlay = playEntity.PlayType == "kickoff" || playEntity.PlayType == "punt";

            var play = new Play
            {
                Id = playEntity.Id,
                HomeScore = playEntity.TotalHomeScore ?? 0,
                AwayScore = playEntity.TotalAwayScore ?? 0,
                Description = playEntity.Desc,
                Quarter = playEntity.Qtr,
                QuarterSecondsRemaining = playEntity.QuarterSecondsRemaining ?? 0,
                Game = new Game() {
                    Id = playEntity.GameId,
                    HomeTeam = playEntity.HomeTeam,
                    AwayTeam = playEntity.AwayTeam,
                    Week = playEntity.Week,
                },
                HomePlayLog = MapPlayStats(playEntity, playEntity.TotalHomeScore, isOffensivePlay, isSpecialTeamsPlay, playEntity.IsHomeTeamReceivingKickoffOrPunt),
                AwayPlayLog = MapPlayStats(playEntity, playEntity.TotalAwayScore, isOffensivePlay, isSpecialTeamsPlay, playEntity.IsAwayTeamReceivingKickoffOrPunt),
            };

            return play;
        }

        public static Stat MapStatModel(StatEntity statEntity)
        {
            if (statEntity == null) return null;

            var stat = new Stat
            {
                GameId = statEntity.GameId,
                Team = statEntity.Team,
                AirYards = statEntity.AirYards,
                Sacks = statEntity.Sacks,
                Score = statEntity.Score,
                Punts = statEntity.Punts,
                ReturnYards = statEntity.ReturnYards
            };

            return stat;
        }

        private static PlayLog MapPlayStats(PlayEntity playEntity, int? score, bool isOffensivePlay, bool isSpecialTeamsPlay, bool isReceivingKickoffOrPunt)
        {
            return new PlayLog()
            {
                Score = (int)score,
                OffensePlayLog = isOffensivePlay && !isSpecialTeamsPlay ? new OffensePlayLog()
                {
                    AirYards = playEntity.AirYards ?? 0
                }
                :
                null,
                DefensePlayLog = !isOffensivePlay && !isSpecialTeamsPlay ? new DefensePlayLog()
                {
                    Sacks = playEntity.Sack ?? 0
                }
                :
                null,
                SpecialPlayLog = isSpecialTeamsPlay ? new SpecialPlayLog()
                {
                    ReturnYards = isReceivingKickoffOrPunt ? playEntity.ReturnYards ?? 0 : 0,
                    Punts = !isReceivingKickoffOrPunt && Convert.ToBoolean(playEntity.PuntAttempt) ? 1 : 0
                }
                :
                null
            };
        }
    }
}