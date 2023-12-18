using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers;

public class MyAdressController : Controller
{
    private readonly ILogger<MyAdressController> _logger;

    public MyAdressController(ILogger<MyAdressController> logger)
    {
        _logger = logger;
    }

[Route("/Home/MyAdress")]
    public IActionResult MyAdress()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
