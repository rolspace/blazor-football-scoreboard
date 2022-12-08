using Football.Application.Common.Models;

namespace Football.Application.Games.Queries;

public class GamesVm
{
    public IList<GameDto> Games { get; set; } = new List<GameDto>();
}
