using System;

namespace ContactService.Models;

public enum ContactType
{
    Phone = 1,
    Email = 2,
    Location = 3
}

public class ContactInfo
{
    public Guid Id { get; set; }

    public ContactType Type { get; set; }
    public string Content { get; set; } = null!;

    public Guid PersonId { get; set; }
    public Person Person { get; set; } = null!;
}
