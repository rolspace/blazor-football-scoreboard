using Football.Core.Interfaces.Models;
using Football.Core.Models;
using Football.Core.Persistence.MySql.Entities;
using System;

namespace Football.Core.Persistence.MySql.Utilities
{
    public class ModelMapper
    {
        public static IGame MapGameModel(GameEntity gameEntity)
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

        public static IPlay MapPlayModel(PlayEntity playEntity)
        {
            if (playEntity == null) return null;

            bool isOffense = playEntity.Posteam == playEntity.HomeTeam;

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
                HomePlayData = MapPlayData(playEntity, isOffense),
                AwayPlayData = MapPlayData(playEntity, isOffense),
            };

            return play;
        }

        public static IStat MapStatModel(StatEntity statEntity)
        {
            if (statEntity == null) return null;

            var stat = new Stat
            {
                GameId = statEntity.GameId,
                Team = statEntity.Team,
                AirYards = statEntity.AirYards
            };

            return stat;
        }

        private static PlayData MapPlayData(PlayEntity playEntity, bool isOffense)
        {
            return new PlayData()
            {
                OffensivePlayData = isOffense ? new OffensivePlayData()
                {
                    AirYards = playEntity.AirYards ?? 0
                }
                :
                null,
                DefensivePlayData = !isOffense ? new DefensivePlayData()
                {
                    Sacks = playEntity.Sack ?? 0
                }
                :
                null
            };
        }
    }
}