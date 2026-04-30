using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KUETResearchSociety.Data;
using KUETResearchSociety.ViewModels.Admin;

namespace KUETResearchSociety.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new DashboardViewModel
            {
                TotalTeamMembers = await _context.TeamMembers.CountAsync(),
                TotalActivities = await _context.Activities.CountAsync(),
                TotalEvents = await _context.Events.CountAsync(),
                TotalGalleryItems = await _context.GalleryItems.CountAsync(),
                TotalMessages = await _context.ContactMessages.CountAsync(),
                UnreadMessages = await _context.ContactMessages.CountAsync(m => !m.IsRead),
                RecentMessages = await _context.ContactMessages
                    .OrderByDescending(m => m.CreatedAt)
                    .Take(5)
                    .ToListAsync()
            };

            return View(vm);
        }
    }
}
