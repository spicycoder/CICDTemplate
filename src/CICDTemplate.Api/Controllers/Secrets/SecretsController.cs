using CICDTemplate.Application.Secrets.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CICDTemplate.Api.Controllers.Secrets;

[Route("api/secrets")]
[ApiController]
public class SecretsController(ISender sender) : ControllerBase
{
    [HttpGet("read")]
    public async Task<ActionResult<string>> Read(string key)
    {
        var command = new ReadSecretCommand(key);
        var result = await sender.Send(command);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}