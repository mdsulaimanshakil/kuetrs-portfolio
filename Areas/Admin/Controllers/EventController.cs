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
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ImageService _imageService;

        public EventController(ApplicationDbContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.OrderByDescending(e => e.EventDate).ToListAsync();
            return View(events);
        }

        public IActionResult Create() => View(new Event { EventDate = DateTime.Now.AddDays(7) });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event model, IFormFile? bannerFile)
        {
            if (!ModelState.IsValid) return View(model);

            if (bannerFile != null)
            {
                var path = await _imageService.SaveImageAsync(bannerFile, "events");
                if (path != null) model.BannerImage = path;
            }

            model.CreatedAt = DateTime.UtcNow;
            _context.Events.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Event added successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event model, IFormFile? bannerFile)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var existing = await _context.Events.FindAsync(id);
            if (existing == null) return NotFound();

            if (bannerFile != null)
            {
                _imageService.DeleteImage(existing.BannerImage);
                var path = await _imageService.SaveImageAsync(bannerFile, "events");
                if (path != null) existing.BannerImage = path;
            }

            existing.Title = model.Title;
            existing.Description = model.Description;
            existing.EventDate = model.EventDate;
            existing.Venue = model.Venue;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Event updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return Json(new { success = false });

            _imageService.DeleteImage(ev.BannerImage);
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
