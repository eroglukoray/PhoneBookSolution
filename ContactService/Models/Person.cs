using System;
using System.Collections.Generic;

namespace ContactService.Models;

public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Company { get; set; } = null!;

    public ICollection<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
}