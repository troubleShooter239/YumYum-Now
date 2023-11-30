using MongoDB.Bson;

namespace MVCWebApp.Models.User;

public struct CreditCard
{
    byte CardNumber { get; set; }
    byte ExpMonth { get; set; }
    byte ExpYear { get; set; }
    byte CVV { get; set; }
}

public class User
{
    public ObjectId Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string DeliveryAddress { get; set; }
    public required byte PhoneNumber { get; set; }
    public CreditCard CardNumber { get; set; }
    public required byte[] ProfilePicture { get; set; }
}
