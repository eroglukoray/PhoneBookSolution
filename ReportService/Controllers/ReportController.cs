using Microsoft.AspNetCore.Mvc;

namespace ReportService.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;

    public ReportController(ILogger<ReportController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetReport")]
    public string Get()
    {
      
       return string.Empty;
    }
}
