using System.ComponentModel.DataAnnotations;

namespace KUETResearchSociety.Models
{
    public class SiteSettings
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string SiteTitle { get; set; } = "KUET Research Society";

        [MaxLength(300)]
        public string HeroTitle { get; set; } = "KUET Research Society";

        [MaxLength(500)]
        public string HeroSubtitle { get; set; } = "Innovating Through Research and Technology";

        [MaxLength(300)]
        public string HeroButtonPrimary { get; set; } = "Join Us";

        [MaxLength(300)]
        public string HeroButtonSecondary { get; set; } = "Learn More";

        public string AboutText { get; set; } = string.Empty;

        public string VisionText { get; set; } = string.Empty;

        public string MissionText { get; set; } = string.Empty;

        [MaxLength(300), EmailAddress]
        public string ContactEmail { get; set; } = "info@kuetrs.org";

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(500)]
        public string Address { get; set; } = "KUET Campus, Khulna, Bangladesh";

        [MaxLength(500)]
        public string? FacebookLink { get; set; }

        [MaxLength(500)]
        public string? LinkedInLink { get; set; }

        [MaxLength(500)]
        public string? YoutubeLink { get; set; }

        [MaxLength(500)]
        public string? LogoPath { get; set; }
    }
}
