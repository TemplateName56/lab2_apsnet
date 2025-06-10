using lab2.Models;
using lab2.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lab2.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _context;
        private readonly UserContext _userContext;

        public HomeController(MyDbContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public IActionResult Index()
        {
            if (_context == null)
            {
                throw new Exception("DbContext is null!");
            }
            ViewData["User"] = _userContext.GetUser(_context);
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
