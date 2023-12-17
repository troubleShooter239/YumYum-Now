namespace MVCWebApp.Models;

public class RegisterViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public byte[] ProfilePicture { get; set; } = [];
}
