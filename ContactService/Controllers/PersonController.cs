using ContactService.Data;
using ContactService.DTOs.Person;
using ContactService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public PersonController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonRequest request)
    {
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Surname = request.Surname,
            Company = request.Company
        };

        await _dbContext.Persons.AddAsync(person);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = person.Id }, new PersonResponse
        {
            Id = person.Id,
            Name = person.Name,
            Surname = person.Surname,
            Company = person.Company
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var person = await _dbContext.Persons
            .Include(p => p.ContactInfos)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (person == null) return NotFound();

        return Ok(new
        {
            person.Id,
            person.Name,
            person.Surname,
            person.Company,
            ContactInfos = person.ContactInfos.Select(c => new
            {
                c.Id,
                c.Type,
                c.Content
            })
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var persons = await _dbContext.Persons.ToListAsync();

        return Ok(persons.Select(p => new PersonResponse
        {
            Id = p.Id,
            Name = p.Name,
            Surname = p.Surname,
            Company = p.Company
        }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var person = await _dbContext.Persons.FindAsync(id);
        if (person == null) return NotFound();

        _dbContext.Persons.Remove(person);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}
