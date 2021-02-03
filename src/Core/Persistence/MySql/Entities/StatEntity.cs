namespace Football.Core.Persistence.MySql.Entities
{
    public partial class StatEntity
    {
        public int GameId { get; set; }
        public string Team { get; set; }
        public int AirYards { get; set; }
    }
}
