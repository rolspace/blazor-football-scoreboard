using Football.Core.Interfaces.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Football.Core.Persistence.Interfaces.DataProviders
{
    public interface IFootballDataProvider
    {
        Task<ReadOnlyCollection<IGame>> GetGamesByWeek(int week);

        Task<ReadOnlyCollection<IPlay>> GetPlaysByGameTime(int week, int gameSecondsRemainingStart, int gameSecondsRemainingEnd);

        Task SaveStat(IStat stat);
    }
}
