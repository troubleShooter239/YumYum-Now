using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;
using MVCWebApp.Tools.Interfaces;

namespace MVCWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEmailVerification _emailVerification;
    public HomeController(ILogger<HomeController> logger, IEmailVerification emailVerification)
    {
        _logger = logger;
        _emailVerification = emailVerification;
    }

    public async Task<IActionResult> Index()
    {
        // var receiver = "receiver@mail.com";
        // var subject = "Mail title.";
        // var message = "Mail text.";

        // await _emailVerification.SendAsync(receiver, subject, message);

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Reviews() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
