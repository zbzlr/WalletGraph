using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
					return RedirectToAction("Graphs", new { userId = user.UserId });
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

        [HttpPost]
        public IActionResult AddNewExpenditure(Expenditure expenditure)
        {
			User user = dbContext.Users.SingleOrDefault(u => u.UserId == expenditure.UserId);
			expenditure.User = user;

			ModelState.Remove("User");

			if (ModelState.IsValid)
			{
				dbContext.Add(expenditure);
				dbContext.SaveChanges();
			}
			else
			{
				var errors = ModelState.SelectMany(x => x.Value.Errors.Select(p => p.ErrorMessage)).ToList();
				return BadRequest(new { errors });
			}

			return RedirectToAction("Graphs", new { userId = expenditure.UserId });
		}

        [HttpGet]
        public IActionResult Graphs(int? userId)
        {
            User user = dbContext.Users.SingleOrDefault(u => u.UserId == userId);
            ViewBag.User = user;
            ViewBag.UserId = userId;
            ViewBag.CategoryPercents = CalculateCategoryPercents(user);
            ViewBag.ExpensesByDays = DetermineExpensesByDays(user);

            return View();
        }

		private List<int> CalculateCategoryPercents(User user)
		{
			List<string> categories = new List<string> { "Food & Beverage", "Housing", "Transportation", "Clothing & Accessories", "Entertainment & Hobbies" };
			List<int> categoryAmountSummary = new List<int>(); 
			List<int> categoryPercents = new List<int>(); 
			int totalExpenditureAmount = 0; 

			// Calculate total amount for each category
			foreach (string category in categories)
			{
				decimal totalAmount = dbContext.Expenditures
					.Where(e => e.UserId == user.UserId && e.Category == category)
					.Sum(e => e.Amount);

				int totalAmountInt = (int)totalAmount;

				categoryAmountSummary.Add(totalAmountInt);
			}


            totalExpenditureAmount = CalculateSumOfExpenses(categoryAmountSummary);
			
            ViewBag.Total = totalExpenditureAmount;

            if(totalExpenditureAmount == 0)
            {
                return new List<int> {0,0,0,0,0};
            }

			// Calculate percentage for each category
			foreach (int categoryTotal in categoryAmountSummary)
			{
				int categoryPercent = (int)((decimal)categoryTotal / totalExpenditureAmount * 100);
				categoryPercents.Add(categoryPercent);
			}

			return categoryPercents; 
		}

        private int CalculateSumOfExpenses(List<int> expenses)
        {
            int totalExpenditureAmount = 0;
			foreach (int amount in expenses)
			{
				totalExpenditureAmount += amount;
			}
            return totalExpenditureAmount;
		}

		public class ExpenseByDay
		{
			public DayOfWeek Day { get; set; }
			public int TotalExpense { get; set; }
		}

		private List<ExpenseByDay> DetermineExpensesByDays(User user)
		{
			var expenses = dbContext.Expenditures
		.Where(e => e.UserId == user.UserId)
		.ToList(); 

			var expensesByDays = expenses
				.GroupBy(e => e.Date.DayOfWeek)
				.Select(g => new ExpenseByDay
				{
					Day = g.Key,
					TotalExpense = (int)g.Sum(e => e.Amount)
				})
				.ToList();

			for (int i = 0; i < 7; i++)
			{
				var dayOfWeek = (DayOfWeek)i;

				if (!expensesByDays.Any(e => e.Day == dayOfWeek))
				{
					expensesByDays.Add(new ExpenseByDay
					{
						Day = dayOfWeek,
						TotalExpense = 0
					});
				}
			}

			expensesByDays = expensesByDays.OrderBy(e => e.Day).ToList();

			return expensesByDays;
		}







		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}