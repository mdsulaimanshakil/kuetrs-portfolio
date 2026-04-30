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
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ImageService _imageService;

        public SettingsController(ApplicationDbContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<IActionResult> Edit()
        {
            var settings = await _context.SiteSettings.FirstOrDefaultAsync() ?? new SiteSettings();
            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SiteSettings model, IFormFile? logoFile)
        {
            if (!ModelState.IsValid) return View(model);

            var existing = await _context.SiteSettings.FirstOrDefaultAsync();
            if (existing == null)
            {
                if (logoFile != null)
                    model.LogoPath = await _imageService.SaveImageAsync(logoFile, "logos");
                _context.SiteSettings.Add(model);
            }
            else
            {
                if (logoFile != null)
                {
                    _imageService.DeleteImage(existing.LogoPath);
                    existing.LogoPath = await _imageService.SaveImageAsync(logoFile, "logos");
                }

                existing.SiteTitle = model.SiteTitle;
                existing.HeroTitle = model.HeroTitle;
                existing.HeroSubtitle = model.HeroSubtitle;
                existing.HeroButtonPrimary = model.HeroButtonPrimary;
                existing.HeroButtonSecondary = model.HeroButtonSecondary;
                existing.AboutText = model.AboutText;
                existing.VisionText = model.VisionText;
                existing.MissionText = model.MissionText;
                existing.ContactEmail = model.ContactEmail;
                existing.Phone = model.Phone;
                existing.Address = model.Address;
                existing.FacebookLink = model.FacebookLink;
                existing.LinkedInLink = model.LinkedInLink;
                existing.YoutubeLink = model.YoutubeLink;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Site settings updated successfully!";
            return RedirectToAction(nameof(Edit));
        }
    }
}
