using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab2.Models;
using lab2.Utils;

namespace lab2.Controllers
{
    public class AdminQuestionsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly UserContext _userContext;

        public AdminQuestionsController(MyDbContext context, UserContext userContext)
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
            var user = _userContext.GetUser(_context);
            if (!IsAdmin())
                return StatusCode(403, "Доступ лише для адміністратора.");

            var questionsList = await _context.Questions.Include(q => q.User).ToListAsync();
            return View(questionsList);
        }

        [HttpPost]
        public async Task<IActionResult> Answer(long questionId, string answer)
        {
            if (!IsAdmin())
                return StatusCode(403, "Доступ лише для адміністратора.");

            if (!string.IsNullOrWhiteSpace(answer))
            {
                var question = await _context.Questions.FindAsync(questionId);
                if (question != null)
                {
                    question.Answer = answer.Trim();
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
