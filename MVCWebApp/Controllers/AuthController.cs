using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;
using MVCWebApp.Models.User;
using MVCWebApp.Services.EncryptorService;
using MVCWebApp.Services.HasherService;
using MVCWebApp.Services.UserService;

namespace MVCWebApp;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly IPasswordHasher _hasher;
    private readonly IAesEncryptor _encryptor;
    private readonly IUserService _userService;

    public AuthController(ILogger<AuthController> logger, IUserService userService, 
        IPasswordHasher hasher, IAesEncryptor encryptor)
    {
        _logger = logger;
        _hasher = hasher;
        _encryptor = encryptor;
        _userService = userService;
    }

    // Displays the login form.
    [HttpGet]
    public IActionResult Login() => View();

    // Handles the login form submission.
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogInformation("Invalid model.");
            return View(model);
        }

        var email = _encryptor.EncryptString(model.Email);
        var phone = _encryptor.EncryptString(model.PhoneNumber);

        if (await _userService.GetByEmail(email) != null || 
            await _userService.GetByPhone(phone) != null)
        {
            _logger.LogInformation("User with this email and/or phone is already registered.");
            return View(model);
        }

        var user = new User
        {
            Email = email,
            PhoneNumber = phone,
            PasswordHash = _hasher.HashString(model.Password)
        };

        if (user is null)
        {
            _logger.LogInformation("Invalid login attempt");
            return View(model);
        }

        if (user.PasswordHash != _hasher.HashString(model.Password))
        {
            ModelState.AddModelError(string.Empty, "Invalid password");
            _logger.LogInformation($"{user.Id}: Invalid password");
            return View(model);
        }
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            // Add another claims
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        try
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(claimsIdentity));
        }
        catch(Exception ex)
        {
            // Log exception
            _logger.LogError(ex, "Error during user sign in");
            throw; // Re-throw the exception to ensure proper error handling
        }

        // Log successful login
        _logger.LogInformation($"User logged in: {model.Email}");

        // Redirect to the main page
        return RedirectToAction("Index", "Home");
    }

    // Displays the registration form.
    [HttpGet]
    public IActionResult Register() => View();

    // Handles the registration form submission.
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid) 
        {
            _logger.LogInformation("Invalid model.");
            return View(model);
        }

        var email = _encryptor.EncryptString(model.Email);
        var phone = _encryptor.EncryptString(model.PhoneNumber);

        // Check if a user with the same email or phone already exists
        if (await _userService.GetByEmail(email) != null || 
            await _userService.GetByPhone(phone) != null)
        {
            _logger.LogInformation("User with this email and/or phone is already registered.");
            return View(model);
        }
        
        // Creating a new instance of user
        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = email,
            PasswordHash = _hasher.HashString(model.Password),
            DeliveryAddress = _encryptor.EncryptString(model.DeliveryAddress),
            PhoneNumber = phone,
            ProfilePicture = model.ProfilePicture
        };

        // Check if user creation was successful
        if (user is null)
        {
            _logger.LogError("Error with creating user.");
            return View(model);
        }

        // Save the user to the database
        await _userService.Create(user);

        // Log successful registration
        _logger.LogInformation($"User registered: {user.Id}");

        // Redirect to login page
        return RedirectToAction("Login");        
    }
}
