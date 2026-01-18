using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterQualityMonitoring.Models;
using System.Linq;

namespace WaterQualityMonitoring.Controllers
{
    public class ReportsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reports
        public IActionResult Index()
        {
            var reports = _context.WaterReports
               .Include(r => r.User)
               .OrderByDescending(r => r.ReportDate)
               .ToList();

            return View(reports);

        }

        // GET: Create Report
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: Create Report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WaterReport report)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetString("UserId");
                report.UserID = int.Parse(userId);
                report.ReportDate = DateTime.Now;

                _context.WaterReports.Add(report);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(report);
        }

        // GET: Search Reports
        public IActionResult Search(string location, string pollutionType)
        {
            ViewBag.Location = location;
            ViewBag.PollutionType = pollutionType;

            var reports = _context.WaterReports
       .Include(r => r.User)
       .AsQueryable();


            if (!string.IsNullOrEmpty(location))
            {
                reports = reports.Where(r => r.Location.Contains(location));
            }

            if (!string.IsNullOrEmpty(pollutionType) && pollutionType != "All")
            {
                reports = reports.Where(r => r.PollutionType == pollutionType);
            }

            return View(reports.OrderByDescending(r => r.ReportDate).ToList());
        }

        //  Report Details
        public IActionResult Details(int id)
        {
            var report = _context.WaterReports
                .Include(r => r.User)
                .FirstOrDefault(r => r.ReportID == id);

            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }



    }
}
