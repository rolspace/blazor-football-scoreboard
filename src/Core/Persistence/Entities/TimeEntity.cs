using System.Collections.Generic;

namespace Football.Core.Persistence.Entities
{
    public class TimeEntity
    {
        public int GameId { get; set; }

        public int Quarter { get; set; }

        public int QuarterSecondsRemaining { get; set; }

        public virtual GameEntity Game { get; set; }

        public virtual ICollection<StatEntity> Stat { get; set; }
    }
}
