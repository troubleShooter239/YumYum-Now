using MVCWebApp.Models.ProductDB;

namespace MVCWebApp.Models.Cart;

public class OrderHistory : IOrderHistory
{
    public List<Product> Products { get; set; } = [];
}
