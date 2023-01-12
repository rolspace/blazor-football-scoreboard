using Microsoft.AspNetCore.Mvc;

namespace Football.Api.Controllers;

[ApiController]
[Route("api/games")]
public class GameController : ApiControllerBase
{
    public GameController()
    {
    }

    [HttpGet("{gameId}")]
    public async Task<ActionResult> GetGame(int gameId)
    {
        return Ok("works");
    }
}
