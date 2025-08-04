using Shared.Enums;

namespace ReportService.DTOs.Report;

public class ReportListItemDto
{
    public Guid Id { get; set; }
    public DateTime RequestDate { get; set; }
    public ReportStatus Status { get; set; }
}
