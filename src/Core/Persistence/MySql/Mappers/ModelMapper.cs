using Football.Core.Interfaces;
using Football.Core.Models;
using Football.Core.Persistence.MySql.Entities;

namespace Football.Core.Persistence.MySql.Mappers
{
    public class ModelMapper
    {
        public ModelMapper()
        {
        }

        public static IGame MapGameModel(GameEntity gameEntity)
        {
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
            var play = new Play
            {
                Id = playEntity.Id,
                GameId = playEntity.GameId,
                HomeTeam = playEntity.HomeTeam,
                HomeScore = playEntity.TotalHomeScore ?? 0,
                AwayTeam = playEntity.AwayTeam,
                AwayScore = playEntity.TotalAwayScore ?? 0,
                Description = playEntity.Desc,
                Quarter = playEntity.Qtr,
                QuarterSecondsRemaining = playEntity.QuarterSecondsRemaining ?? 0
            };

            return play;
        }

        public static IStat MapStatModel(StatEntity statEntity)
        {
            var stat = new Stat
            {
                GameId = statEntity.GameId,
                Team = statEntity.Team,
                AirYards = statEntity.AirYards
            };

            return stat;
        }
    }
}
