using System;
using System.ComponentModel.DataAnnotations;

namespace KUETResearchSociety.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required, MaxLength(300)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Image { get; set; }

        public string? Details { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
