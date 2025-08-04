
using System.ComponentModel.DataAnnotations;

namespace ContactService.Models;

public class Person
{
    public Guid Id { get; set; }
  
    public required string Name { get; set; }
   
    public required string Surname { get; set; }
    public string Company { get; set; } = null!;

    public ICollection<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
}