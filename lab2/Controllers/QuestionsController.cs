using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab2.Models;
using lab2.Utils;

namespace lab2.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly UserContext _userContext;

        public QuestionsController(MyDbContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<IActionResult> Index()
        {
            var user = _userContext.GetUser(_context);

            var questions = await _context.Questions
                .Include(q => q.User)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();

            ViewBag.CurrentUser = user;
            return View(questions);
        }

        [HttpPost]
        public async Task<IActionResult> Ask(string question)
        {
            var user = _userContext.GetUser(_context);
            if (user == null || string.IsNullOrWhiteSpace(question))
                return RedirectToAction("Index");

            var newQuestion = new Question
            {
                Text = question.Trim(),
                User = user
            };

            _context.Questions.Add(newQuestion);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Questions");
        }
    }
}
