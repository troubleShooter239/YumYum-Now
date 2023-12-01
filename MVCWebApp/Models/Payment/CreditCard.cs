namespace MVCWebApp.Models.Payment;

public struct CreditCard
{
    public byte CardNumber { get; set; }
    public byte ExpMonth { get; set; }
    public byte ExpYear { get; set; }
    public byte CVV { get; set; }
}
