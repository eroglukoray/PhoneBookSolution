namespace ContactService.DTOs.ContactInfo;

public class AddContactInfoRequest
{
    public int Type { get; set; } // 1 = Phone, 2 = Email, 3 = Location
    public string Content { get; set; } = null!;
}
