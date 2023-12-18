using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCWebApp.Controllers;


[Authorize]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    public IActionResult Cart() => View();
    public IActionResult Profile() => View();
    public IActionResult OrderHistory() => View();
}
