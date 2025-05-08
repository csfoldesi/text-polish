using Application.Test;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TestController : BaseApiController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello");
    }

    [HttpPost]
    public async Task<IActionResult> Test()
    {
        var result = await Mediator.Send(new Get.Query { });
        return Ok(result);
    }
}
