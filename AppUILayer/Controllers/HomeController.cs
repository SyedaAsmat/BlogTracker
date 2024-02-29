using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BlogTracker.Models;
using Microsoft.EntityFrameworkCore;
using APPUILayer.Data;

namespace WebAppPlayers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly APPUIDbContext _context;
        private readonly APPUIDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, APPUIDbContext context, APPUIDbContext dbContext)//, EmpInfo empData)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.BlogInfo != null ?
                        View(await _context.BlogInfo.ToListAsync()) :
                        Problem("Entity set 'BlogTrackerDbContext.BlogInfo'  is null.");
        }
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(string email, string password)
        {
            var admin = dbContext.AdminInfo.FirstOrDefault(a => a.EmailId == email && a.Password == password);
            try
            {
                if (admin != null)
                {
                    if (admin.Password == password)
                    {
                        TempData["ErrorSuccess"] = "Welcome Admin";
                        // Authentication successful, redirect to a secure area or dashboard
                        return RedirectToAction("EmployeeList", "Admin");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid username or password";
                        return RedirectToAction("AdminLogin", "Home");
                    }
                }
            }
            
            catch(Exception ex)
            {
                // Authentication failed, show an error message
                ViewBag.ErrorMessage = ex.Message;
                return View("AdminLogin","Home");
            }
            return View();
        }

        public IActionResult EmployeeList()
        {
            return RedirectToAction("EmployeeList", "Admin");
        }

        [HttpGet]
        public IActionResult EmployeeLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeLogin(string email, int password)
        {
            /*if (!string.IsNullOrEmpty(email))
            {
                TempData["SuccessMessage"] = "Welcome Employee";
                return RedirectToAction("BlogList", "Employee");
            }*/
            var emp = dbContext.EmpInfo.FirstOrDefault(a => a.EmailId == email && a.PassCode == password);
            try
            {
                if (emp != null)
                {
                    if (emp.PassCode == password)
                    {
                        TempData["SuccessMessage"] = "Welcome Employee";
                        return RedirectToAction("BlogList", "Employee");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid username or password";
                        return RedirectToAction("EmployeeLogin", "Home");
                    }
                }
            }

            catch (Exception ex)
            {
                // Authentication failed, show an error message
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("AdminLogin", "Home");
            }
            return View();
        }

        public IActionResult BlogList()
        {
            return RedirectToAction("BlogList", "Employee");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
