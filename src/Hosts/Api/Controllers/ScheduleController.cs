using Microsoft.AspNetCore.Mvc;

namespace Football.Api.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController : ApiControllerBase
{
    [HttpGet("{weekNumber}")]
    public async Task<ActionResult> GetSchedule(int weekNumber)
    {
        return Ok("works");
    }
}
