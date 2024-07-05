using Microsoft.AspNetCore.Mvc;

namespace CICDTemplate.Api.Controllers.Bindings;

[Route("api/bindings")]
[ApiController]
public class BindingsController(ILogger<BindingsController> logger) : ControllerBase
{
    [HttpPost("cron")]
    public IActionResult Cron()
    {
        logger.LogInformation("Cron job executed");
        return Ok();
    }
}
