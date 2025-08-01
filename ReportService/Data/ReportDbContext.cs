using Microsoft.EntityFrameworkCore;
using ReportService.Models;

namespace ReportService.Data;

public class ReportDbContext : DbContext
{
    public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options) { }

    public DbSet<Report> Reports => Set<Report>();
    public DbSet<ReportDetail> ReportDetails => Set<ReportDetail>();
}
