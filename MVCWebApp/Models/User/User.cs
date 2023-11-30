using MongoDB.Bson;

namespace MVCWebApp.Models.User;

public class User
{
    public ObjectId Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string DeliveryAddress { get; set; }
    public required string PhoneNumber { get; set; }
    public required byte[] ProfilePicture { get; set; }
}
