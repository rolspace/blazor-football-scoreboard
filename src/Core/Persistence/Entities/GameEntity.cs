using System.Collections.Generic;

namespace Football.Core.Persistence.Entities
{
    public class GameEntity
    {
        public GameEntity()
        {
            Plays = new HashSet<PlayEntity>();
            Stats = new HashSet<StatEntity>();
        }

        public int Id { get; set; }

        public int Week { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public virtual TimeEntity Time { get; set; }

        public virtual ICollection<PlayEntity> Plays { get; set; }

        public virtual ICollection<StatEntity> Stats { get; set; }
    }
}
