namespace MVCWebApp.Models.ProductDB;

public interface IProduct
{
    string Id { get; set; }
    string Category { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    Dictionary<int, float> PortionPrice { get; set; }
    byte[] ImageUrl { get; set; }
}
