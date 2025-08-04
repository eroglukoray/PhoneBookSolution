using ContactService.Data;
using ContactService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Tests.Unit.Statistics
{
    public class LocationStatisticsTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void GetLocationStatistics_ShouldReturnCorrectCounts()
        {
            // Arrange
            var context = GetDbContext();

            var person = new Models.Person
            {
                Id = Guid.NewGuid(),
                Name = "Koray",
                Surname = "Eroglu",
                Company = "StatsCorp",
                ContactInfos = new List<Models.ContactInfo>
                {
                    new() { Id = Guid.NewGuid(), Type = ContactType.Location, Content = "Istanbul" },
                    new() { Id = Guid.NewGuid(), Type = ContactType.Phone, Content = "123-4567" }
                }
            };

            context.Persons.Add(person);
            context.SaveChanges();

            // Act
            var location = "Istanbul";

            var personCount = context.Persons
                .Count(p => p.ContactInfos.Any(c => c.Type == ContactType.Location && c.Content == location));

            var phoneNumberCount = context.ContactInfos
                .Count(c => c.Type == ContactType.Phone &&
                            c.Person.ContactInfos.Any(loc => loc.Type == ContactType.Location && loc.Content == location));

            // Assert
            Assert.Equal(1, personCount);
            Assert.Equal(1, phoneNumberCount);
        }
    }
}
