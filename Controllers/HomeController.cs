using LaLaFaMusicPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LaLaFaMusicPortal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}