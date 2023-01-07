using Microsoft.AspNetCore.Mvc;

namespace Football.Host.Api.Controllers;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
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
