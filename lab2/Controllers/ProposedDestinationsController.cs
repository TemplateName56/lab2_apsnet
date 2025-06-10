using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab2.Models;
using lab2.Utils;

namespace lab2.Controllers
{
    public class ProposedDestinationsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly UserContext _userContext;

        public ProposedDestinationsController(MyDbContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["User"] = _userContext.GetUser(_context);
            var proposals = await _context.ProposedDestinations.ToListAsync();
            return View(proposals);
        }

        [HttpPost]
        public async Task<IActionResult> Vote(long id)
        {
            var user = _userContext.GetUser(_context);
            if (user == null)
                return StatusCode(403, "Необхідно увійти.");

            var dest = await _context.ProposedDestinations.FindAsync(id);
            if (dest != null)
            {
                dest.VoteCount++;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name, string description)
        {
            var user = _userContext.GetUser(_context);
            if (user == null)
                return StatusCode(403, "Необхідно увійти.");

            if (!string.IsNullOrWhiteSpace(name))
            {
                var newProposal = new ProposedDestination
                {
                    Name = name.Trim(),
                    Description = description?.Trim()
                };
                _context.ProposedDestinations.Add(newProposal);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
