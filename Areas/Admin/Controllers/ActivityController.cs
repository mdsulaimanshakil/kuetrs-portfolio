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
    public class ActivityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ImageService _imageService;

        public ActivityController(ApplicationDbContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            var activities = await _context.Activities.OrderByDescending(a => a.CreatedAt).ToListAsync();
            return View(activities);
        }

        public IActionResult Create() => View(new Activity());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Activity model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid) return View(model);

            if (imageFile != null)
            {
                var path = await _imageService.SaveImageAsync(imageFile, "activities");
                if (path != null) model.Image = path;
            }

            model.CreatedAt = DateTime.UtcNow;
            _context.Activities.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Activity added successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null) return NotFound();
            return View(activity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Activity model, IFormFile? imageFile)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var existing = await _context.Activities.FindAsync(id);
            if (existing == null) return NotFound();

            if (imageFile != null)
            {
                _imageService.DeleteImage(existing.Image);
                var path = await _imageService.SaveImageAsync(imageFile, "activities");
                if (path != null) existing.Image = path;
            }

            existing.Title = model.Title;
            existing.Description = model.Description;
            existing.Details = model.Details;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Activity updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null) return Json(new { success = false });

            _imageService.DeleteImage(activity.Image);
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
