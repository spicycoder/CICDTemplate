using CICDTemplate.Api.Models.Common;
using CICDTemplate.Application.Products.Commands.DeleteState;
using CICDTemplate.Application.States.Commands.SaveState;
using CICDTemplate.Application.States.Queries.ReadState;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CICDTemplate.Api.Controllers.States;

[Route("api/states")]
[ApiController]
public class StatesController(ISender sender) : ControllerBase
{
    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] Product state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var command = new SaveStateCommand(state.Name, state.Description);
        await sender.Send(command);

        return Ok();
    }

    [HttpGet("read")]
    public async Task<ActionResult<ProductState>> Read([FromQuery] string productName)
    {
        ReadStateCommand command = new(productName);
        ProductState? state = await sender.Send(command);

        if (state is null)
        {
            return NotFound();
        }

        return Ok(state!);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] string productName)
    {
        DeleteStateCommand command = new(productName);
        await sender.Send(command);

        return Ok();
    }
}