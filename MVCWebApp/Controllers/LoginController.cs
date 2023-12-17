// using System.Security.Claims;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authentication.Cookies;
// using Microsoft.AspNetCore.Mvc;
// using MVCWebApp.Models;
// using MVCWebApp.Models.User;
// using MVCWebApp.Services.EncryptorService;
// using MVCWebApp.Services.HasherService;
// using MVCWebApp.Services.UserService;

// namespace MVCWebApp.Controllers;

// // FIXME
// public class LoginController : Controller
// {
//     private readonly ILogger<LoginController> _logger;
//     private readonly IUserService _userService;
//     private readonly IAesEncryptor _encryptor;
//     private readonly IPasswordHasher _hasher;

//     public LoginController(ILogger<LoginController> logger, IUserService userService, 
//         IPasswordHasher hasher, IAesEncryptor encryptor)
//     {
//         _logger = logger;
//         _userService = userService;
//         _hasher = hasher;
//         _encryptor = encryptor;
//     }

//     [HttpGet]
//     public IActionResult Login() => View();

//     [HttpPost]
//     public async Task<IActionResult> Login(LoginViewModel model)
//     {
//         if (!ModelState.IsValid)
//         {
//             _logger.LogInformation("Invalid model.");
//             return View(model);
//         }

//         var email = _encryptor.EncryptString(model.Email);
//         var phone = _encryptor.EncryptString(model.PhoneNumber);

//         if (await _userService.GetByEmail(email) != null || 
//             await _userService.GetByPhone(phone) != null)
//         {
//             _logger.LogInformation("User with this email and/or phone is already registered.");
//             return View(model);
//         }

//         var user = new User
//         {
//             Email = email,
//             PhoneNumber = phone,
//             PasswordHash = _hasher.HashString(model.Password)
//         };

//         if (user is null)
//         {
//             _logger.LogInformation("Invalid login attempt");
//             return View(model);
//         }

//         if (user.PasswordHash != _hasher.HashString(model.Password))
//         {
//             ModelState.AddModelError(string.Empty, "Invalid password");
//             _logger.LogInformation($"{user.Id}: Invalid password");
//             return View(model);
//         }
        
//         var claims = new List<Claim>
//         {
//             new Claim(ClaimTypes.Email, user.Email),
//             // Add another claims
//         };

//         var claimsIdentity = new ClaimsIdentity(
//             claims, CookieAuthenticationDefaults.AuthenticationScheme);

//         try
//         {
//             await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
//                                           new ClaimsPrincipal(claimsIdentity));
//         }
//         catch(Exception ex)
//         {
//             // Log exception
//             _logger.LogError(ex, "Error during user sign in");
//             throw; // Re-throw the exception to ensure proper error handling
//         }

//         // Log successful login
//         _logger.LogInformation($"User logged in: {model.Email}");

//         // Redirect to the main page
//         return RedirectToAction("Index", "Home");
//     }
// }
