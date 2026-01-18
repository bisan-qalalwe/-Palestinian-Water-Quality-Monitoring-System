using Microsoft.AspNetCore.Mvc;
using WaterQualityMonitoring.Models;
using System.Security.Cryptography;
using System.Text;      



namespace WaterQualityMonitoring.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        // GET: Register
        public IActionResult Register()
        {
            return View();
        }


        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if username already exists
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "The username already exists.");
                    return View(user);
                }

                // Hash password
                user.Password = HashPassword(user.Password);

                _context.Users.Add(user);
                _context.SaveChanges();

                // Set session
                HttpContext.Session.SetString("UserId", user.UserID.ToString());
                HttpContext.Session.SetString("Username", user.Username);

                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }


        // GET: Login
        public IActionResult Login()
        {
            return View();
        }


        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.UserID.ToString());
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Incorrect username or password");
            return View();
        }


        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //  Profile
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.Find(int.Parse(userId));
            return View(user);
        }

        //  GET Change Password
        public IActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {

            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.Find(int.Parse(userId));

            if (user.Password != HashPassword(oldPassword))
            {
                ModelState.AddModelError("oldPassword", "The old password is incorrect.");
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Passwords do not match");
                return View();
            }

            user.Password = HashPassword(newPassword);
            _context.SaveChanges();

            ViewBag.Message = "The password has been successfully changed.";
            return RedirectToAction("ChangePassword");
        }

        // Helper: Hash Password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }


    }
}
