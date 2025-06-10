using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab2.Models;
using lab2.Utils;

namespace lab2.Controllers
{
    public class BookingsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly UserContext _userContext;

        public BookingsController(MyDbContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        // GET: /Booking
        public async Task<IActionResult> Index()
        {
            var user = _userContext.GetUser(_context);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var bookingsList = await _context.Bookings
                .Include(b => b.Trip)
                    .ThenInclude(t => t.Destination)
                .Where(b => b.User != null && b.User.Id == user.Id)
                .ToListAsync();

            return View(bookingsList);
        }

        // POST: /Booking/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var user = _userContext.GetUser(_context);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var booking = await _context.Bookings
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null || booking.User == null || booking.User.Id != user.Id)
            {
                return StatusCode(403, "Бронювання не знайдено або належить іншому користувачу.");
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
