using System.Diagnostics;
using FitGymMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitGymMVC.Controllers
{
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

        
        public IActionResult Bienvenida()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
