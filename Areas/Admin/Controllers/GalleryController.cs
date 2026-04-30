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
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ImageService _imageService;

        public GalleryController(ApplicationDbContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.GalleryItems.OrderByDescending(g => g.CreatedAt).ToListAsync();
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(List<IFormFile> images, string? category, string? title)
        {
            if (images == null || images.Count == 0)
            {
                TempData["Error"] = "Please select at least one image.";
                return RedirectToAction(nameof(Index));
            }

            int uploaded = 0;
            foreach (var file in images)
            {
                var path = await _imageService.SaveImageAsync(file, "gallery");
                if (path != null)
                {
                    _context.GalleryItems.Add(new GalleryItem
                    {
                        Title = title ?? Path.GetFileNameWithoutExtension(file.FileName),
                        ImagePath = path,
                        Category = category,
                        CreatedAt = DateTime.UtcNow
                    });
                    uploaded++;
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = $"{uploaded} image(s) uploaded successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.GalleryItems.FindAsync(id);
            if (item == null) return Json(new { success = false });

            _imageService.DeleteImage(item.ImagePath);
            _context.GalleryItems.Remove(item);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
