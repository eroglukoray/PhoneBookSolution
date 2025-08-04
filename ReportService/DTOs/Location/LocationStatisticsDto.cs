namespace ReportService.DTOs.Location
{
    public class LocationStatisticsDto
    {
        public string Location { get; set; } = null!;
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }
    }
}
