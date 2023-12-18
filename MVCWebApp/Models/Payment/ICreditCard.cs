namespace MVCWebApp.Models.Payment;

public interface ICreditCard 
{
    int CardNumber { get; set; }
    int ExpMonth { get; set; }
    int ExpYear { get; set; }
    int CVV { get; set; }
}
