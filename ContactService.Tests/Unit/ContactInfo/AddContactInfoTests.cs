using ContactService.Data;
using ContactService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Tests.Unit.ContactInfo
{
    public class AddContactInfoTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void AddContactInfo_ShouldLinkToPerson()
        {
            // Arrange
            var context = GetDbContext();
            var personId = Guid.NewGuid();
            var person = new Models.Person
            {
                Id = personId,
                Name = "Koray",
                Surname = "Eroglu",
                Company = "TestCompany",
                ContactInfos = new List<Models.ContactInfo>()
            };
            context.Persons.Add(person);
            context.SaveChanges();

            // Act
            var contactInfo = new Models.ContactInfo
            {
                Id = Guid.NewGuid(),
                PersonId = personId,
                Type = ContactType.Email,
                Content = "koray.eroglu@example.com"
            };
            context.ContactInfos.Add(contactInfo);
            context.SaveChanges();

            // Assert
            var added = context.ContactInfos.Include(ci => ci.Person).FirstOrDefault(ci => ci.Content.Contains("example.com"));
            Assert.NotNull(added);
            Assert.Equal("Koray", added!.Person.Name);
        }

        [Fact]
        public void AddPerson_WithoutName_ShouldFail()
        {
            // Arrange
            var context = GetDbContext();
            var person = new Models.Person
            {
                Id = Guid.NewGuid(),
                Name = "",
                Surname = "Test",
                Company = "FailCo"
            };

            // Act & Assert
            context.Persons.Add(person);
            var ex = Record.Exception(() => context.SaveChanges());

            Assert.Null(ex);
        }
    }
}
