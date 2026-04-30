using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KUETResearchSociety.Data;
using KUETResearchSociety.Models;
using KUETResearchSociety.Services;

namespace KUETResearchSociety.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TeamController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ImageService _imageService;

        public TeamController(ApplicationDbContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            var members = await _context.TeamMembers.OrderBy(t => t.DisplayOrder).ToListAsync();
            return View(members);
        }

        public IActionResult Create() => View(new TeamMember());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamMember model, IFormFile? photoFile)
        {
            if (!ModelState.IsValid) return View(model);

            if (photoFile != null)
            {
                var path = await _imageService.SaveImageAsync(photoFile, "team");
                if (path != null) model.Photo = path;
            }

            model.CreatedAt = DateTime.UtcNow;
            _context.TeamMembers.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Team member added successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var member = await _context.TeamMembers.FindAsync(id);
            if (member == null) return NotFound();
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeamMember model, IFormFile? photoFile)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var existing = await _context.TeamMembers.FindAsync(id);
            if (existing == null) return NotFound();

            if (photoFile != null)
            {
                _imageService.DeleteImage(existing.Photo);
                var path = await _imageService.SaveImageAsync(photoFile, "team");
                if (path != null) existing.Photo = path;
            }

            existing.Name = model.Name;
            existing.Designation = model.Designation;
            existing.Department = model.Department;
            existing.Batch = model.Batch;
            existing.Facebook = model.Facebook;
            existing.LinkedIn = model.LinkedIn;
            existing.Bio = model.Bio;
            existing.DisplayOrder = model.DisplayOrder;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Team member updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _context.TeamMembers.FindAsync(id);
            if (member == null) return Json(new { success = false });

            _imageService.DeleteImage(member.Photo);
            _context.TeamMembers.Remove(member);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
