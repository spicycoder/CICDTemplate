using CICDTemplate.Api.Models.Common;
using CICDTemplate.Domain.Abstract;

using Dapr;

using Microsoft.AspNetCore.Mvc;

namespace CICDTemplate.Api.Controllers.Subscriber;

[Route("api/[controller]")]
[ApiController]
public class SubscriberController(ILogger<SubscriberController> logger) : ControllerBase
{
    [HttpPost("consume")]
    [Topic(Constants.PubSubName, Constants.PubSubTopicName)]
    public IActionResult Subscribe(
        [FromBody] Product message,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Received message: {Message}", message);
        return Ok();
    }
}