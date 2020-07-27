namespace Core.Entities
{
    public class Game : BaseEntity
    {
        public int Id { get; set; }
        public int Week { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
    }
}
