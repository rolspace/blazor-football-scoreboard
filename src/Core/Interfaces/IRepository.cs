using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Football.Core.Interfaces
{
    public interface IRepository
    {
        Task<ReadOnlyCollection<IGame>> GetGamesByWeek(int week);

        Task<ReadOnlyCollection<IPlay>> GetPlaysByGameTime(int week, int gameSecondsRemainingStart, int gameSecondsRemainingEnd);

        Task<ReadOnlyCollection<IStat>> GetStatsByGameAndTeam(int gameId, string team);
    }
}
