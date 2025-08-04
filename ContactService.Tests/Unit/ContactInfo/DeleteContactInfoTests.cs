using ContactService.Data;
using ContactService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Tests.Unit.ContactInfo
{
    public class DeleteContactInfoTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void DeleteContactInfo_ShouldRemoveSuccessfully()
        {
            // Arrange
            var context = GetDbContext();
            var personId = Guid.NewGuid();
            var contactInfoId = Guid.NewGuid();

            var person = new Models.Person
            {
                Id = personId,
                Name = "Koray",
                Surname = "Eroglu",
                Company = "TestCompany",
                ContactInfos = new List<Models.ContactInfo>
                {
                    new Models.ContactInfo
                    {
                        Id = contactInfoId,
                        Type = ContactType.Phone,
                        Content = "555-1234"
                    }
                }
            };

            context.Persons.Add(person);
            context.SaveChanges();

            // Act
            var contactInfo = context.ContactInfos.FirstOrDefault(ci => ci.Id == contactInfoId);
            context.ContactInfos.Remove(contactInfo!);
            context.SaveChanges();

            // Assert
            var deleted = context.ContactInfos.FirstOrDefault(ci => ci.Id == contactInfoId);
            Assert.Null(deleted);
        }
    }
}
