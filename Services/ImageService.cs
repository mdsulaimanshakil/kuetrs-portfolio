namespace KUETResearchSociety.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string?> SaveImageAsync(IFormFile file, string subfolder)
        {
            if (file == null || file.Length == 0)
                return null;

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(ext))
                return null;

            var uploadDir = Path.Combine(_env.WebRootPath, "uploads", subfolder);
            if (!Directory.Exists(uploadDir))
                Directory.CreateDirectory(uploadDir);

            var uniqueName = $"{Guid.NewGuid()}{ext}";
            var fullPath = Path.Combine(uploadDir, uniqueName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/{subfolder}/{uniqueName}";
        }

        public void DeleteImage(string? relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return;

            var fullPath = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/'));
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}
