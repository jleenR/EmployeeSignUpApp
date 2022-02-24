using Microsoft.AspNetCore.Mvc;
using EmployeeSignUpApp.Models;
using System.Diagnostics;
using EmployeeSignUpApp.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSignUpApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmployeeDBContext _context;

        public HomeController(ILogger<HomeController> logger, EmployeeDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(Employee loggedinemployee)
        {
            var employee = _context.Employees
                .Include(co => co.Gender).Include(co => co.MaritalStatus)
                .FirstOrDefault(s => s.EmployeeId == loggedinemployee.EmployeeId);
            return View(employee);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            return RedirectToAction();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}