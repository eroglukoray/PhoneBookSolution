using ContactService.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Tests.Unit.Person
{
    public class AddPersonTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void AddPerson_ShouldSaveSuccessfully()
        {
            // Arrange
            var context = GetDbContext();
            var person = new Models.Person
            {
                Id = Guid.NewGuid(),
                Name = "Koray",
                Surname = "Eroğlu",
                Company = "ErogluCo"
            };

            // Act
            context.Persons.Add(person);
            context.SaveChanges();

            // Assert
            var result = context.Persons.FirstOrDefault(p => p.Name == "Koray");
            Assert.NotNull(result);
            Assert.Equal("Eroğlu", result!.Surname);
        }
    }
}