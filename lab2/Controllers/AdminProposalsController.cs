using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab2.Models;
using lab2.Utils;

namespace lab2.Controllers
{
    public class AdminProposalsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly UserContext _userContext;

        public AdminProposalsController(MyDbContext context, UserContext userContext)
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
            {
                return StatusCode(403, "Доступ лише для адміністратора.");
            }

            var proposals = await _context.ProposedDestinations.ToListAsync();
            return View(proposals);
        }

        [HttpPost]
        public async Task<IActionResult> HandleAction(long id, string action)
        {
            if (!IsAdmin())
            {
                return StatusCode(403, "Доступ лише для адміністратора.");
            }

            var proposal = await _context.ProposedDestinations.FindAsync(id);
            if (proposal == null)
            {
                return RedirectToAction("Index");
            }

            if (action == "approve")
            {
                var destination = new Destination
                {
                    Name = proposal.Name,
                    Description = proposal.Description
                };

                _context.Destinations.Add(destination);
                _context.ProposedDestinations.Remove(proposal);
            }
            else if (action == "delete")
            {
                _context.ProposedDestinations.Remove(proposal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
