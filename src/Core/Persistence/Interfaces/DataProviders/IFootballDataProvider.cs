using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Football.Core.Models;

namespace Football.Core.Persistence.Interfaces.DataProviders
{
    public interface IFootballDataProvider
    {
        Task<Game> GetGame(int gameId);

        Task<ReadOnlyCollection<Game>> GetGamesByWeek(int week);

        Task<ReadOnlyCollection<Play>> GetPlaysByWeekAndGameTime(int week, int gameSecondsRemainingStart, int gameSecondsRemainingEnd);

        Task<ReadOnlyCollection<Stat>> GetGameStats(int gameId);

        Task SaveStat(int gameId, string team, PlayLog playLog);
    }
}
