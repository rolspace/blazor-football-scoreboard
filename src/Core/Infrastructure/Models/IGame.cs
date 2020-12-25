namespace Core.Infrastructure.Models
{
    public interface IGame
    {
        public int Id { get; set; }

        public int Week { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }
    }
}
