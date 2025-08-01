using ContactService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Events;

namespace ContactService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportRequestPublisher _publisher;

    public ReportController(IReportRequestPublisher publisher)
    {
        _publisher = publisher;
    }

    [HttpPost("request")]
    public async Task<IActionResult> RequestReport()
    {
        var reportId = Guid.NewGuid();

        var requestEvent = new ReportRequestedEvent
        {
            ReportId = reportId,
            RequestedAt = DateTime.UtcNow
        };

        await _publisher.PublishReportRequestAsync(requestEvent);

        return Accepted(new
        {
            Message = "Report request published to RabbitMQ",
            ReportId = reportId
        });
    }
}
