namespace ContactService.DTOs.Person
{
    public class CreatePersonRequest
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Company { get; set; } = null!;

    }
}
