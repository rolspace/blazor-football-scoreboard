namespace Core.Infrastructure.MySql.Entities
{
    public class Game : Entity
    {
        public int Week { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
    }
}
