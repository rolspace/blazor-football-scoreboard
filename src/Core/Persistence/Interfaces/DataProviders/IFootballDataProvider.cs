using Football.Core.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Football.Core.Persistence.Interfaces.DataProviders
{
    public interface IFootballDataProvider
    {
        Task<ReadOnlyCollection<Game>> GetGamesByWeek(int week);

        Task<ReadOnlyCollection<Play>> GetPlaysByWeekAndGameTime(int week, int gameSecondsRemainingStart, int gameSecondsRemainingEnd);

        Task SaveStat(Stat stat);
    }
}
