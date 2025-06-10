using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab2.Models;
using lab2.Utils;

namespace lab2.Controllers
{
    public class TripsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly UserContext _userContext;

        public TripsController(MyDbContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<IActionResult> Index()
        {
            var user = _userContext.GetUser(_context);
            var trips = await _context.Trips.Include(t => t.Destination).ToListAsync();

            var bookedTripIds = user != null
                ? await _context.Bookings
                    .Where(b => b.User.Id == user.Id)
                    .Select(b => b.Trip.Id)
                    .ToListAsync()
                : new List<long>();

            ViewBag.CurrentUser = user;
            ViewBag.BookedTripIds = bookedTripIds;

            return View(trips);
        }

        [HttpPost]
        public async Task<IActionResult> Book(long tripId)
        {
            var user = _userContext.GetUser(_context);
            if (user == null)
                return StatusCode(403, "Ви не авторизовані!");

            var trip = await _context.Trips.FindAsync(tripId);
            if (trip != null)
            {
                var booking = new Booking
                {
                    User = user,
                    Trip = trip,
                    BookingDate = DateTime.Now
                };
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
