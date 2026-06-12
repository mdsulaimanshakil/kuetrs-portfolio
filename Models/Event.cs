using System;
using System.ComponentModel.DataAnnotations;

namespace KUETResearchSociety.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required, MaxLength(300)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime EventDate { get; set; }

        [MaxLength(300)]
        public string? Venue { get; set; }

        [MaxLength(500)]
        public string? BannerImage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
