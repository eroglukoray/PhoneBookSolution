using System.Net.Http.Json;
using MassTransit;
using ReportService.Data;
using ReportService.Models;
using Shared.Events;
using Shared.Enums;
using ReportService.DTOs.Location;

namespace ReportService.Consumers;

public class ReportRequestedConsumer : IConsumer<ReportRequestedEvent>
{
    private readonly ReportDbContext _dbContext;
    private readonly IHttpClientFactory _httpClientFactory;

    public ReportRequestedConsumer(ReportDbContext dbContext, IHttpClientFactory httpClientFactory)
    {
        _dbContext = dbContext;
        _httpClientFactory = httpClientFactory;
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

        Console.WriteLine($"[CONSUMER] Report created with ID: {report.Id}");

        var httpClient = _httpClientFactory.CreateClient("ContactService");
        var statistics = await httpClient.GetFromJsonAsync<List<LocationStatisticsDto>>("/api/statistics/location-statistics");

        if (statistics != null && statistics.Count > 0)
        {
            foreach (var item in statistics)
            {
                var detail = new ReportDetail
                {
                    Id = Guid.NewGuid(),
                    ReportId = report.Id,
                    Location = item.Location,
                    PersonCount = item.PersonCount,
                    PhoneNumberCount = item.PhoneNumberCount
                };

                await _dbContext.ReportDetails.AddAsync(detail);
                Console.WriteLine($"[DETAIL] {item.Location} - {item.PersonCount} persons, {item.PhoneNumberCount} phones");
            }

            report.Status = ReportStatus.Completed;
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine("[WARN] No statistics found or ContactService failed.");
        }
    }
}
