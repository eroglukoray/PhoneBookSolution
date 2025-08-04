using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportService.Data;
using ReportService.DTOs.Report;

namespace ReportService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly ReportDbContext _dbContext;

    public ReportController(ReportDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reports = await _dbContext.Reports
            .OrderByDescending(r => r.RequestDate)
            .ToListAsync();

        var result = reports.Select(r => new ReportListItemDto
        {
            Id = r.Id,
            RequestDate = r.RequestDate,
            Status = r.Status
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var report = await _dbContext.Reports
            .Include(r => r.Details)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (report == null)
            return NotFound();

        var result = report.Details.Select(d => new ReportDetailDto
        {
            Id = report.Id,
            RequestDate = report.RequestDate,
            Location = d.Location,
            PersonCount = d.PersonCount,
            PhoneNumberCount = d.PhoneNumberCount
        });

        return Ok(result);
    }
}
