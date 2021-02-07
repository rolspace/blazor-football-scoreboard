using System.Collections.Generic;

namespace Football.Core.Persistence.MySql.Entities
{
    public class GameEntity
    {
        public int Id { get; set; }

        public int Week { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public virtual ICollection<PlayEntity> Play { get; set; }

        public virtual ICollection<StatEntity> Stat { get; set; }
    }
}
