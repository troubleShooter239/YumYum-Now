using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MVCWebApp.Models;
using MVCWebApp.Models.User;
using MVCWebApp.Services;
using MVCWebApp.Tools.Encrypters;
using MVCWebApp.Tools.Hashers;

namespace MVCWebApp.Controllers;

public class RegisterController : Controller
{
    private readonly ILogger<RegisterController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHasher _hasher;
    private readonly IEncrypter _encrypter;
    private readonly IUserService _userService;

    // TODO: for _hasher, _encrypter create singletons instead
    public RegisterController(ILogger<RegisterController> logger, IConfiguration configuration, IUserService userService)
    {
        _logger = logger;
        _configuration = configuration;
        _hasher = new PasswordHasher(_configuration);
        _encrypter = new AesEncrypter(_configuration);
        _userService = userService;
    }
    
    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) 
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError($"ModelState error: {error.ErrorMessage}");
            }
            _logger.LogInformation("Invalid");
            return View(model);
        }
        
        _logger.LogInformation("Model valid");

        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = _encrypter.EncryptString(model.Email),
            PasswordHash = _hasher.HashString(model.Password),
            DeliveryAddress = _encrypter.EncryptString(model.DeliveryAddress),
            PhoneNumber = _encrypter.EncryptString(model.PhoneNumber),
            ProfilePicture = model.ProfilePicture
        };

        if (user == null)
        {
            _logger.LogError("Error with creating user.");
            return View(model);
        }

        await _userService.Create(user);

        // Log successful registration
        _logger.LogInformation($"User registered: {user.Id}");

        // Redirect to login page
        return RedirectToAction("Login");        
    }
}
