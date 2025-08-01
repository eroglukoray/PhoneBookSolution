using MassTransit;
using ReportService.Data;
using ReportService.Models;
using Shared.Events;
using Shared.Enums;

namespace ReportService.Consumers;

public class ReportRequestedConsumer : IConsumer<ReportRequestedEvent>
{
    private readonly ReportDbContext _dbContext;

    public ReportRequestedConsumer(ReportDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<ReportRequestedEvent> context)
    {
        var message = context.Message;

        var report = new Report
        {
            Id = message.ReportId,
            RequestDate = message.RequestedAt,
            Status = ReportStatus.Preparing
        };

        await _dbContext.Reports.AddAsync(report);
        await _dbContext.SaveChangesAsync();

        Console.WriteLine($"📩 Report received and saved. ID: {report.Id}");
    }
}
