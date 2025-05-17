using API.Dto;
using Application.Text;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TextController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> FixText(TextFixRequest request)
    {
        var result = await Mediator.Send(new Fix.Query { Text = request.Text });
        return Ok(result);
    }
}
