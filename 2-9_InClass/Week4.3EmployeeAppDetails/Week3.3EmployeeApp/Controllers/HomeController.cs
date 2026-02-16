using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Week3._3EmployeeApp.Models;

namespace Week3._3EmployeeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() //Generating the Index View
        {
            return View();
        }

        public IActionResult Privacy() //Generating the Privacy View
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() //Generating the Error View
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}