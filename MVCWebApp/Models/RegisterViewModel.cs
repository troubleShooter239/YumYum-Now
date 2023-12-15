using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MVCWebApp.Models;

public class RegisterViewModel
{
    // [BsonId]
    // [BsonRepresentation(BsonType.ObjectId)]
    // public string Id { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string DeliveryAddress { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public byte[] ProfilePicture { get; set; } = [];
}
