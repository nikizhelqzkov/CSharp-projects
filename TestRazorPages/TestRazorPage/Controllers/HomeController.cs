using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestRazorPage.Models;

namespace TestRazorPage.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Test()
        {
            return View(new List<DataViewModel>()
            {
                new DataViewModel { Id = 0, Name = "test", Age = 13 },
                new DataViewModel { Id = 1, Name = "test2", Age = 12}
            });
        }
    }
}