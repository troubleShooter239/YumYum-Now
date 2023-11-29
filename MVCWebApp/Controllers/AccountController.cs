using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MVCWebApp.Models;
using MVCWebApp.Models.User;
using MVCWebApp.Tools;

namespace MVCWebApp.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMongoCollection<User> _userCollection;

    public AccountController(ILogger<HomeController> logger, IMongoCollection<User> userCollection)
    {
        _logger = logger;
        _userCollection = userCollection;
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Check if the user has already registered
        var existingUser = await (await _userCollection.FindAsync(u => 
            u.Email == model.Email)).FirstOrDefaultAsync();
        if (existingUser != null)
        {
            ModelState.AddModelError("Email", "User with this email already exists");
            return View(model);
        }

        // Creating new user
        var newUser = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PasswordHash = PasswordHasher.HashString(model.Password),
            DeliveryAddress = model.DeliveryAddress,
            PhoneNumber = model.PhoneNumber,
            ProfilePicture = model.ProfilePicture
        };

        try
        {
            // Saving user in database
            await _userCollection.InsertOneAsync(newUser);
        }
        catch (Exception ex)
        {
            // Log exception
            _logger.LogError(ex, "Error during user registration");
            throw; // Re-throw the exception to ensure proper error handling
        }

        // Log successful registration
        _logger.LogInformation($"User registered: {model.Email}");

        // Redirect to login page
        return RedirectToAction("Login");        
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Check if user already logged in
        var user = _userCollection.Find(u => 
            u.Email == model.Email && 
            u.PasswordHash == PasswordHasher.HashString(model.Password)
        ).FirstOrDefault();

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(model);
        }
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            // Add another claims
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = model.RememberMe
        };

        try
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(claimsIdentity),
                                          authProperties);
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
}
