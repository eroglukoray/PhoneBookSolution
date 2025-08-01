using Shared.Enums;

namespace ReportService.Models;

public class Report
{
    public Guid Id { get; set; }
    public DateTime RequestDate { get; set; }
    public ReportStatus Status { get; set; }

    public ICollection<ReportDetail> Details { get; set; } = new List<ReportDetail>();
}
