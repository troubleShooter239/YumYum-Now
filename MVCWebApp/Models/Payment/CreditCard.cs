namespace MVCWebApp.Models.Payment;

public struct CreditCard : ICreditCard
{
    public int CardNumber { get; set; }
    public int ExpMonth { get; set; }
    public int ExpYear { get; set; }
    public int CVV { get; set; }
}
