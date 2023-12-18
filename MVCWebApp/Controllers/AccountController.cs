using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models.JWTSettings;
using MVCWebApp.Services.JWTService;

namespace MVCWebApp.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IJwtService _jwtService;

    public AccountController(ILogger<AccountController> logger, IJwtService jwtService)
    {
        _logger = logger;
        _jwtService = jwtService;
    }

    public IActionResult Cart()
    {
        var jwtCookie = Request.Cookies["JwtCookie"];
        if (jwtCookie == null || !_jwtService.Validate(jwtCookie)) 
            return RedirectToAction("Login", "Account");

        return View();
    }

    public IActionResult Profile() 
    {
        var jwtCookie = Request.Cookies["JwtCookie"];
        if (jwtCookie == null || !_jwtService.Validate(jwtCookie)) 
            return RedirectToAction("Login", "Account");

        return View();
    }

    public IActionResult OrderHistory()
    {
        var jwtCookie = Request.Cookies["JwtCookie"];
        if (jwtCookie == null || !_jwtService.Validate(jwtCookie)) 
            return RedirectToAction("Login", "Account");

        return View();
    }
}
