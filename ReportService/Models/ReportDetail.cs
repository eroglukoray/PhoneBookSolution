namespace ReportService.Models;

public class ReportDetail
{
    public Guid Id { get; set; }
    public Guid ReportId { get; set; }
    public string Location { get; set; } = null!;
    public int PersonCount { get; set; }
    public int PhoneNumberCount { get; set; }

    public Report Report { get; set; } = null!;
}
