using Microsoft.AspNetCore.Mvc;
using lab2.Utils;

namespace lab2.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly MyDbContext _context;

        public DestinationsController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var destinationsList = _context.Destinations.ToList();
            return View(destinationsList);
        }
    }
}
