﻿namespace Football.Core.Persistence.MySql.Entities
{
    public class StatEntity
    {
        public int GameId { get; set; }
        public string Team { get; set; }
        public int AirYards { get; set; }

        public virtual GameEntity Game { get; set; }
    }
}