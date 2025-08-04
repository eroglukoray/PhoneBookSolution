namespace ReportService.DTOs.Report;

public class ReportDetailDto
{
    public Guid Id { get; set; }
    public DateTime RequestDate { get; set; }
    public string Location { get; set; } = null!;
    public int PersonCount { get; set; }
    public int PhoneNumberCount { get; set; }
}
