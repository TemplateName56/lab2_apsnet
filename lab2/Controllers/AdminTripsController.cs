using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab2.Models;
using lab2.Utils;

namespace lab2.Controllers
{
    public class AdminTripsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly UserContext _userContext;

        public AdminTripsController(MyDbContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }
        private bool IsAdmin()
        {
            var user = _userContext.GetUser(_context);
            return user != null && user.Admin;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!IsAdmin())
                return StatusCode(403, "Доступ лише для адміністратора.");

            ViewBag.Destinations = await _context.Destinations.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(long destinationId, DateTime date, double price)
        {
            if (!IsAdmin())
                return StatusCode(403, "Доступ лише для адміністратора.");

            var destination = await _context.Destinations.FindAsync(destinationId);
            if (destination == null)
                return BadRequest("Напрямок не знайдено.");

            var trip = new Trip
            {
                Destination = destination,
                Date = date,
                Price = price
            };

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Trips"); // або інша сторінка поїздок
        }
    }
}
