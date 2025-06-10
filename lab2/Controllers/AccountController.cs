using Microsoft.AspNetCore.Mvc;
using lab2.Models;
using lab2.Utils;

namespace lab2.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDbContext _context;
        private readonly UserContext _userContext;


        public AccountController(MyDbContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _userContext.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            string passwordHash = Utilities.GenerateHash(password);

            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == passwordHash);

            if (user == null)
            {
                ViewBag.Error = "Невірне ім'я користувача або пароль.";
                return View();
            }

            _userContext.SetUser(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                ViewBag.Error = "Користувач із таким ім’ям вже існує.";
                return View();
            }

            var user = new User
            {
                Username = username,
                Password = Utilities.GenerateHash(password),
                Admin = false
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            _userContext.SetUser(user);

            return RedirectToAction("Index", "Home");
        }
    }
}
