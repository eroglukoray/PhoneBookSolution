using ContactService.Services.Interfaces;
using MassTransit;
using Shared.Events;

namespace ContactService.Services;

public class ReportRequestPublisher : IReportRequestPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ReportRequestPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishReportRequestAsync(ReportRequestedEvent requestEvent)
    {
        await _publishEndpoint.Publish(requestEvent);
    }
}
