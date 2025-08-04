using ContactService.Models;

namespace ContactService.Data.Seed
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Persons.Any()) return; // Zaten veri varsa geç

            var persons = new List<Person>
        {
            new Person
            {
                Id = Guid.NewGuid(),
                Name = "Koray",
                Surname = "Eroglu",
                Company = "ACME Inc",
                ContactInfos = new List<ContactInfo>
                {
                    new ContactInfo
                    {
                        Id = Guid.NewGuid(),
                        Type = ContactType.Phone,
                        Content = "+905551112233"
                    },
                    new ContactInfo
                    {
                        Id = Guid.NewGuid(),
                        Type = ContactType.Email,
                        Content = "eroglu@acme.com"
                    },
                    new ContactInfo
                    {
                        Id = Guid.NewGuid(),
                        Type = ContactType.Location,
                        Content = "Istanbul"
                    }
                }
            },
            new Person
            {
                Id = Guid.NewGuid(),
                Name = "Simge",
                Surname = "Sagın",
                Company = "Beta Corp",
                ContactInfos = new List<ContactInfo>
                {
                    new ContactInfo
                    {
                        Id = Guid.NewGuid(),
                        Type = ContactType.Phone,
                        Content = "+905554445566"
                    },
                    new ContactInfo
                    {
                        Id = Guid.NewGuid(),
                        Type = ContactType.Location,
                        Content = "Ankara"
                    }
                }
            }
        };

            context.Persons.AddRange(persons);
            context.SaveChanges();
        }
    }
}
