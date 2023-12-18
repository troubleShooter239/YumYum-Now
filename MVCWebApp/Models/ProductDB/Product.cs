using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MVCWebApp.Models.ProductDB;

public class Product : IProduct
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    [BsonElement("category")]
    public string Category { get; set; } = string.Empty;
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;
    [BsonElement("portion_prices")]
    public Dictionary<string, double> PortionPrice { get; set; } = [];
    [BsonElement("image_url")]
    public string ImageUrl { get; set; } = string.Empty;
}
