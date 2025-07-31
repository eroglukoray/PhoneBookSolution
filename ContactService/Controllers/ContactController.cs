using Microsoft.AspNetCore.Mvc;

namespace ContactService.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{

    [HttpGet(Name = "GetContacs")]
    public string Get()
    {
        return "everybody";
    }
}
