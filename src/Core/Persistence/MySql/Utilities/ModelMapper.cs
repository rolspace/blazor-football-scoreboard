using Football.Core.Interfaces.Models;
using Football.Core.Models;
using Football.Core.Persistence.MySql.Entities;
using System;

namespace Football.Core.Persistence.MySql.Utilities
{
    public class ModelMapper
    {
        public ModelMapper()
        {
        }

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
                PlayStats = new PlayStats()
                {
                    Home = new TeamStats()
                    {
                        Team = playEntity.HomeTeam,
                        Score = playEntity.TotalHomeScore ?? 0,
                        DefenseStats = new DefenseStats()
                        {
                            Sacks = playEntity.Defteam == playEntity.HomeTeam ? (Convert.ToBoolean(playEntity.Sack) ? 1 : 0) : 0,
                        },
                        OffenseStats = new OffenseStats()
                        {
                            AirYards = playEntity.Posteam == playEntity.HomeTeam ? playEntity.AirYards ?? 0 : 0,
                        }
                    }
                }
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
    }
}
