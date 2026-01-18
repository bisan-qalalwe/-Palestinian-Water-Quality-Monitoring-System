using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WaterQualityMonitoring.Models;

namespace WaterQualityMonitoring.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
           
           // عرض آخر 10 تقارير

            var recentReports = _context.WaterReports
                .OrderByDescending(r => r.ReportDate)
                .Take(10)
                .ToList();

            return View(recentReports);
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
