using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers;

public class ConfidentialityController : Controller
{
    private readonly ILogger<ConfidentialityController> _logger;

    public ConfidentialityController(ILogger<ConfidentialityController> logger)
    {
        _logger = logger;
    }

[Route("/Home/Confidentiality")]
    public IActionResult Confidentiality()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
