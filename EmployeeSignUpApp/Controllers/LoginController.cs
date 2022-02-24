using EmployeeSignUpApp.Models;
using EmployeeSignUpApp.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSignUpApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly EmployeeDBContext _context;

        public LoginController(EmployeeDBContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            var genders = _context.Genders.Select(x => x).ToList();
            var mysignupmodel = new SignUpModel();
            var emptygenderlist = new List<SelectListItem>();
            var emptymaritalstat = new List<SelectListItem>();
            mysignupmodel.Genderlist = emptygenderlist;
            mysignupmodel.MaritalStatuslist = emptymaritalstat;
            foreach (var item in genders)
            {
                mysignupmodel.Genderlist.Add(new SelectListItem() { Text = item.GenderDesc, Value = item.GenderId.ToString() });
            }

            var Maritalstat = _context.MaritalStatuses.Select(x => x).ToList();

            foreach (var item in Maritalstat)
            {
                mysignupmodel.MaritalStatuslist.Add(new SelectListItem() { Text = item.MaritalStatusDesc, Value = item.MaritalStatusId.ToString() });
            }

            return View(mysignupmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logon(SignUpModel mysignUpModel)
        {
            var checkifuserexists = _context.Credentials.Where(x => x.UserName == mysignUpModel.Email && x.Password == mysignUpModel.Password).FirstOrDefault();
            if (checkifuserexists != null)
            {
                //var employee = _context.Employees.Select(s => s.EmployeeId == checkifuserexists.EmployeeId);
                var employee = _context.Employees
                .Include(co => co.Gender).Include(co => co.MaritalStatus)
                .FirstOrDefault(s => s.EmployeeId == checkifuserexists.EmployeeId);
                return RedirectToAction("Index", "Home", employee);
            }
            else
            {
                ViewBag.ErrorMessage = "Match not found. Please retry or create an Account.";
                return View("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(SignUpModel mysignUpModel)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Credentials.FirstOrDefault(s => s.UserName == mysignUpModel.Email);

                if (check == null)
                {
                    long maxemployeeId;
                    var Existingentries = _context.Employees.Select(s => s).ToList();
                    if (Existingentries.Count > 0)
                    {
                        maxemployeeId = _context.Employees.Max(s => s.EmployeeId);
                    }
                    else
                    {
                        maxemployeeId = 0;
                    }

                    Employee employeeobject = new Employee();
                    employeeobject.EmployeeId = maxemployeeId + 1;
                    employeeobject.FirstName = (mysignUpModel.FirstName);
                    employeeobject.LastName = (mysignUpModel.LastName);
                    employeeobject.Email = (mysignUpModel.Email);
                    employeeobject.GenderId = (mysignUpModel.GenderId);
                    employeeobject.MaritalStatusId = (mysignUpModel.MaritalStatusId);
                    _context.Add(employeeobject);
                    _context.SaveChanges();

                    Credential credentialobject = new Credential();
                    credentialobject.EmployeeId = employeeobject.EmployeeId;
                    credentialobject.UserName = mysignUpModel.Email;
                    credentialobject.Password = mysignUpModel.Password;
                    _context.Add(credentialobject);
                    _context.SaveChanges();

                    //return View("Home/Index");
                    return RedirectToAction("Index", "Home", employeeobject);
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }
    }
}