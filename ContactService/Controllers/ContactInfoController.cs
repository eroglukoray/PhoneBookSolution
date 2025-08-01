using ContactService.Data;
using ContactService.DTOs.ContactInfo;
using ContactService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactInfoController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ContactInfoController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Kişiye iletişim bilgisi ekle
    [HttpPost("{personId}")]
    public async Task<IActionResult> AddContactInfo(Guid personId, [FromBody] AddContactInfoRequest request)
    {
        var person = await _dbContext.Persons.FindAsync(personId);
        if (person == null)
            return NotFound("Person not found");

        var contactInfo = new ContactInfo
        {
            Id = Guid.NewGuid(),
            Type = (ContactType)request.Type,
            Content = request.Content,
            PersonId = personId
        };

        await _dbContext.ContactInfos.AddAsync(contactInfo);
        await _dbContext.SaveChangesAsync();

        return Ok(new
        {
            contactInfo.Id,
            contactInfo.Type,
            contactInfo.Content
        });
    }

    // İletişim bilgisini sil
    [HttpDelete("{contactInfoId}")]
    public async Task<IActionResult> DeleteContactInfo(Guid contactInfoId)
    {
        var contactInfo = await _dbContext.ContactInfos.FindAsync(contactInfoId);
        if (contactInfo == null)
            return NotFound();

        _dbContext.ContactInfos.Remove(contactInfo);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}
