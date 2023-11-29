using MongoDB.Bson;

namespace MVCWebApp.Models.User;

public class User
{
    public ObjectId Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string DeliveryAddress { get; set; }
    public string PhoneNumber { get; set; }
    public byte[] ProfilePicture { get; set; }
}
