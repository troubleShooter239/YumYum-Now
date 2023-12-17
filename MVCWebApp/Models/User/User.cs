using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MVCWebApp.Models.Payment;

namespace MVCWebApp.Models.User;

[BsonIgnoreExtraElements]
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("first_name")]
    public string FirstName { get; set; } = string.Empty;

    [BsonElement("last_name")]
    public string LastName { get; set; } = string.Empty;
  
    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;
    
    [BsonElement("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;
  
    [BsonElement("delivery_address")]
    public string DeliveryAddress { get; set; } = string.Empty;

    [BsonElement("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [BsonElement("card_details")]
    public CreditCard CardDetails { get; set; }

    [BsonElement("profile_picture")]
    public byte[] ProfilePicture { get; set; } = [];
}
