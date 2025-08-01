using Shared.Events;

namespace ContactService.Services.Interfaces;

public interface IReportRequestPublisher
{
    Task PublishReportRequestAsync(ReportRequestedEvent requestEvent);
}
