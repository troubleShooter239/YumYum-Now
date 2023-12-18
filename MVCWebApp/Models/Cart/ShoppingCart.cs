using MVCWebApp.Models.ProductDB;

namespace MVCWebApp.Models.Cart;

public class ShoppingCart : IShoppingCart
{
    public List<Product> Products { get; set; } = [];
}
