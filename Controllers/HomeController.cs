using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KUETResearchSociety.Data;
using KUETResearchSociety.Models;
using KUETResearchSociety.ViewModels;

namespace KUETResearchSociety.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _context.SiteSettings.FirstOrDefaultAsync() ?? new SiteSettings();
            var teamMembers = await _context.TeamMembers.OrderBy(t => t.DisplayOrder).ThenBy(t => t.CreatedAt).ToListAsync();
            var activities = await _context.Activities.OrderByDescending(a => a.CreatedAt).Take(6).ToListAsync();
            var events = await _context.Events.Where(e => e.EventDate >= DateTime.UtcNow).OrderBy(e => e.EventDate).Take(3).ToListAsync();
            var gallery = await _context.GalleryItems.OrderByDescending(g => g.CreatedAt).Take(8).ToListAsync();

            var vm = new HomeViewModel
            {
                Settings = settings,
                TeamMembers = teamMembers,
                Activities = activities,
                UpcomingEvents = events,
                GalleryItems = gallery
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact([FromBody] ViewModels.ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, message = string.Join(" ", errors) });
            }

            var msg = new ContactMessage
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message
            };

            _context.ContactMessages.Add(msg);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Your message has been sent successfully!" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
