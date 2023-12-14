using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MVCWebApp.Models;
using MVCWebApp.Models.User;
using MVCWebApp.Tools.Interfaces;

namespace MVCWebApp.Controllers;

public class RegisterController : Controller
{
    private readonly ILogger<RegisterController> _logger;
    private readonly IMongoCollection<User> _userCollection;
    private readonly IConfiguration _configuration;
    private readonly IHasher _hasher;
    private readonly IEncrypter _encrypter;

    public RegisterController(ILogger<RegisterController> logger, IMongoCollection<User> userCollection,
        IConfiguration configuration)
    {
        _logger = logger;
        _userCollection = userCollection;
        _configuration = configuration;
        _hasher = new PasswordHasher(_configuration);
        _encrypter = new AesEncrypter(_configuration);
    }

    // Test endpoint to check if the controller is working
    [HttpGet]
    public IActionResult Test()
    {
        _logger.LogInformation("Test endpoint hit successfully.");
        return Ok("Test endpoint hit successfully.");
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]  
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) 
        {
            return View(model);
        }
        
        // Check if the user has already registered
        // TODO: add phone number to validation
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
            Email = _encrypter.EncryptString(model.Email),
            PasswordHash = _hasher.HashString(model.Password),
            DeliveryAddress = _encrypter.EncryptString(model.DeliveryAddress),
            PhoneNumber = _encrypter.EncryptString(model.PhoneNumber),
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
        _logger.LogInformation($"User registered: {newUser.Id}");

        // Redirect to login page
        // return RedirectToAction("Login");  
        return Ok();      
    }
}
