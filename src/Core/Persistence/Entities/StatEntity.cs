﻿namespace Football.Core.Persistence.Entities
{
    public class StatEntity
    {
        public int GameId { get; set; }

        public string Team { get; set; }

        public int Score { get; set; }

        public int AirYards { get; set; }

        public int Punts { get; set; }

        public int Sacks { get; set; }

        public int ReturnYards { get; set; }

        public virtual GameEntity Game { get; set; }

        public virtual TimeEntity Time { get; set; }
    }
}