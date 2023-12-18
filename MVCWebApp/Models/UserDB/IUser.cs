using MVCWebApp.Models.Cart;
using MVCWebApp.Models.Payment;

namespace MVCWebApp.Models.UserDB;

public interface IUser
{
    string Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    string PasswordHash { get; set; }
    string DeliveryAddress { get; set; }
    string PhoneNumber { get; set; }
    OrderHistory HistoryOrders { get; set; }
    ShoppingCart ShoppingCart { get; set; }
    CreditCard CardDetails { get; set; }
    byte[] ProfilePicture { get; set; }
}
