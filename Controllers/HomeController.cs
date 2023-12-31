using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Libreria.Models;
using Microsoft.AspNetCore.Authorization;

namespace Libreria.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
  [Authorize(Roles = "Administrador, Cliente, Empleado")]
    public IActionResult Index()
    {
        return View();
    }


    [Authorize(Roles = "Administrador, Cliente, Empleado")]
    public IActionResult Productos()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
