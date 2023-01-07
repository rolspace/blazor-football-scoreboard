using Microsoft.AspNetCore.Mvc;

namespace Football.Host.Api.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController : ControllerBase
{
    public ScheduleController()
    {
    }

    [HttpGet("{weekNumber}")]
    public async Task<ActionResult> GetSchedule(int weekNumber)
    {
        return Ok("works");
    }
}
