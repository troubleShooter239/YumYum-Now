using MVCWebApp.Models.ProductDB;

namespace MVCWebApp.Models.Cart;

public interface ICart
{
    List<Product> Products { get; set; }
}
