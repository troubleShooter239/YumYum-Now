using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
      public IActionResult News()
    {
        return View();
    }
       public IActionResult Discounts()
    {
      
       return Redirect("https://habr.com/ru/flows/develop/articles/"); 
    }

    public IActionResult TermsUse() => View();
    public IActionResult PrivacyPolicy() => View();

    public IActionResult ForgotPassword() => View();
        public IActionResult Reviews()
    {
        return View();
    }
        public IActionResult AboutUs()
    {
        return View();
    }
      public IActionResult Map()
    {
        return View();
    }
   



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
