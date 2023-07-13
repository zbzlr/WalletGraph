using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using WalletGraphs.Models;

namespace WalletGraphs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult myWalletPage(int UserId)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User unvalidatedUser)
        {
            if (dbContext.Users.Any(u => u.Email == unvalidatedUser.Email))
            {
                User user = dbContext.Users.FirstOrDefault(u => u.Email == unvalidatedUser.Email);
                if (user.Password == unvalidatedUser.Password)
                {
                    return RedirectToAction("myWalletPage", user.UserId);
                }
            }
            
            TempData["status"] = "Email or Password is Incorrect";
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {

            if(ModelState.IsValid)
            {
                dbContext.Add(user);
                dbContext.SaveChanges();
                TempData["status"] = "Registration successful";
                return RedirectToAction("Index");
            } else 
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    string errorMessage = error.ErrorMessage;
                    // Hata mesajlarını işleyin veya loglayın

                    TempData["status"] = errorMessage;
                }
                

                return RedirectToAction("SignUp"); 
            }
            
            
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            
            return View();
        }

        [AcceptVerbs("GET","POST")]
        public IActionResult HasEmail(string Email)
        {
            var _email = dbContext.Users.Any(u => u.Email == Email);

            if(_email)
            {
                return Json("Email has been registered before");
            }
            else
            {
                return Json(true);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}