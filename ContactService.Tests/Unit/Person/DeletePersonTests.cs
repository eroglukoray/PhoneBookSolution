using ContactService.Data;
using Microsoft.EntityFrameworkCore;


namespace ContactService.Tests.Unit.Person
{
    public class DeletePersonTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void DeletePerson_ShouldRemoveSuccessfully()
        {
            // Arrange
            var context = GetDbContext();
            var person = new Models.Person
            {
                Id = Guid.NewGuid(),
                Name = "Silinecek",
                Surname = "Kişi",
                Company = "TestCorp"
            };
            context.Persons.Add(person);
            context.SaveChanges();

            // Act
            var addedPerson = context.Persons.FirstOrDefault(p => p.Name == "Silinecek");
            context.Persons.Remove(addedPerson!);
            context.SaveChanges();

            // Assert
            var result = context.Persons.FirstOrDefault(p => p.Id == person.Id);
            Assert.Null(result);
        }
    }
}
