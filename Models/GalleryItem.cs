using System;
using System.ComponentModel.DataAnnotations;

namespace KUETResearchSociety.Models
{
    public class GalleryItem
    {
        public int Id { get; set; }

        [Required, MaxLength(300)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string ImagePath { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
