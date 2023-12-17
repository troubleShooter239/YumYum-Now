// using Microsoft.AspNetCore.Mvc;
// using MVCWebApp.Models;
// using MVCWebApp.Models.User;
// using MVCWebApp.Services.EncryptorService;
// using MVCWebApp.Services.HasherService;
// using MVCWebApp.Services.UserService;

// namespace MVCWebApp.Controllers;

// public class RegisterController : Controller
// {
//     private readonly ILogger<RegisterController> _logger;
//     private readonly IPasswordHasher _hasher;
//     private readonly IAesEncryptor _encryptor;
//     private readonly IUserService _userService;

//     public RegisterController(ILogger<RegisterController> logger, IUserService userService, 
//         IPasswordHasher hasher, IAesEncryptor encryptor)
//     {
//         _logger = logger;
//         _hasher = hasher;
//         _encryptor = encryptor;
//         _userService = userService;
//     }

//     // Displays the registration form.
//     [HttpGet]
//     public IActionResult Register() => View();

//     // Handles the registration form submission.
//     [HttpPost]
//     public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
//     {
//         if (!ModelState.IsValid) 
//         {
//             _logger.LogInformation("Invalid model.");
//             return View(model);
//         }

//         var email = _encryptor.EncryptString(model.Email);
//         var phone = _encryptor.EncryptString(model.PhoneNumber);

//         // Check if a user with the same email or phone already exists
//         if (await _userService.GetByEmail(email) != null || 
//             await _userService.GetByPhone(phone) != null)
//         {
//             _logger.LogInformation("User with this email and/or phone is already registered.");
//             return View(model);
//         }
        
//         // Creating a new instance of user
//         var user = new User
//         {
//             FirstName = model.FirstName,
//             LastName = model.LastName,
//             Email = email,
//             PasswordHash = _hasher.HashString(model.Password),
//             DeliveryAddress = _encryptor.EncryptString(model.DeliveryAddress),
//             PhoneNumber = phone,
//             ProfilePicture = model.ProfilePicture
//         };

//         // Check if user creation was successful
//         if (user is null)
//         {
//             _logger.LogError("Error with creating user.");
//             return View(model);
//         }

//         // Save the user to the database
//         await _userService.Create(user);

//         // Log successful registration
//         _logger.LogInformation($"User registered: {user.Id}");

//         // Redirect to login page
//         return RedirectToAction("Login");        
//     }
// }
