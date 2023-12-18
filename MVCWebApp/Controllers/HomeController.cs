using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;
using MVCWebApp.Services.ProductService;

namespace MVCWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;

    public HomeController(ILogger<HomeController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    public IActionResult Privacy() => View();
    public IActionResult News() => View();
    public IActionResult Reviews() => View();
    public IActionResult AboutUs() => View();
    public IActionResult Map() => View();

    // [HttpPost]
    // public async Task<IActionResult> IndexAsync([FromBody] Product product)
    // {
    //     await _productService.Create(product);
    //     return View();
    // }

    [HttpGet]
    public async Task<IActionResult> IndexAsync() 
        => View(new IndexViewModel { Products = await _productService.GetAll()});
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
