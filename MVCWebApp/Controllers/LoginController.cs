using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MVCWebApp.Models;
using MVCWebApp.Models.User;
using MVCWebApp.Tools.Interfaces;

namespace MVCWebApp.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IMongoCollection<User> _userCollection;
    private readonly IConfiguration _configuration;
    private readonly IHasher _hasher;
    private readonly IEncrypter _encrypter;

    public LoginController(ILogger<LoginController> logger, IMongoCollection<User> userCollection,
        IConfiguration configuration)
    {
        _logger = logger;
        _userCollection = userCollection;
        _configuration = configuration;
        _hasher = new PasswordHasher(_configuration);
        _encrypter = new AesEncrypter(_configuration);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var encryptedEmail = _encrypter.EncryptString(model.Email);
        var encryptedPhoneNumber = _encrypter.EncryptString(model.PhoneNumber);

        // Check if user already logged in
        var user = await (await _userCollection.FindAsync(u => 
            u.Email == encryptedEmail || u.PhoneNumber == encryptedPhoneNumber
        )).FirstOrDefaultAsync();

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
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
