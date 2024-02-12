using BulkyBookWeb.Metrics;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OtelMetrics _meters;

        public HomeController(ILogger<HomeController> logger, OtelMetrics metrics)
        {
            _logger = logger;
            _meters = metrics;
        }

        public IActionResult Index()
        {
            _meters.AddBook();
            _meters.IncreaseTotalBooks();
            _meters.RecordNumberOfBooks(1);
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
    }
}