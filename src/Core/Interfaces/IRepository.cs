using Football.Core.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Football.Core.Interfaces
{
    public interface IRepository
    {
        Task<ReadOnlyCollection<IGame>> GetGamesByWeek(int week);

        Task<ReadOnlyCollection<IPlay>> GetPlaysByGameTime(int gameSecondsRemainingStart, int gameSecondsRemainingEnd);
    }
}
