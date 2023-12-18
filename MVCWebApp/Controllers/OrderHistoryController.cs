using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers;

public class OrderHistoryController : Controller
{
    private readonly ILogger<OrderHistoryController> _logger;

    public OrderHistoryController(ILogger<OrderHistoryController> logger)
    {
        _logger = logger;
    }

[Route("/Home/OrderHistory")]
    public IActionResult OrderHistory()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
