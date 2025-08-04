using ContactService.Data;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public StatisticsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("location-statistics")]
    public IActionResult GetLocationStatistics()
    {
        var result = _dbContext.ContactInfos
            .Where(c => c.Type == Models.ContactType.Location)
            .GroupBy(c => c.Content)
            .Select(g => new
            {
                Location = g.Key,
                PersonCount = g.Select(c => c.PersonId).Distinct().Count(),
                PhoneNumberCount = _dbContext.ContactInfos
                    .Where(ci => ci.Type == Models.ContactType.Phone &&
                                 g.Select(gc => gc.PersonId).Contains(ci.PersonId))
                    .Count()
            }).ToList();

        return Ok(result);
    }
}
