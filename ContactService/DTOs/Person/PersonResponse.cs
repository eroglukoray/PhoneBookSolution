namespace ContactService.DTOs.Person
{
    public class PersonResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Company { get; set; } = null!;
    }
}
