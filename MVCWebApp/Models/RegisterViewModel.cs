using System.ComponentModel.DataAnnotations;

namespace MVCWebApp.Models;

public class RegisterViewModel
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Wrong Email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Delivery address is required")]
    public string DeliveryAddress { get; set; }

    [Required(ErrorMessage = "PhoneNumber is required")]
    [Phone(ErrorMessage = "Wrong PhoneNumber")]
    public string PhoneNumber { get; set; }

    public byte[] ProfilePicture { get; set; }
}
