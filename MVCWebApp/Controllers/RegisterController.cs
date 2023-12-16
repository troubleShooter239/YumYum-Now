using Microsoft.AspNetCore.Mvc;
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
    // ! FIXME: encrypter should encrypt text
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
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid) 
        {
            _logger.LogInformation("Invalid model.");
            return View(model);
        }
        if (await _userService.GetByEmail(model.Email) != null || await _userService.GetByPhone(model.PhoneNumber) != null)
        {
            _logger.LogInformation("User with this email and/or phone is already registered.");
            return View(model);
        }

        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PasswordHash = _hasher.HashString(model.Password),
            DeliveryAddress = model.DeliveryAddress,
            PhoneNumber = model.PhoneNumber,
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
