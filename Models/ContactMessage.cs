using System;
using System.ComponentModel.DataAnnotations;

namespace KUETResearchSociety.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(300), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? Subject { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
