using System.ComponentModel.DataAnnotations;

namespace MVCWebApp.Models;

public class RegisterViewModel
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "FirstName is required")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Wrong Email address")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Delivery address is required")]
    public required string DeliveryAddress { get; set; }

    [Required(ErrorMessage = "PhoneNumber is required")]
    [Phone(ErrorMessage = "Wrong PhoneNumber")]
    public required byte PhoneNumber { get; set; }

    public required byte[] ProfilePicture { get; set; }
}
