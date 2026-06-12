using System;
using System.ComponentModel.DataAnnotations;

namespace KUETResearchSociety.Models
{
    public class TeamMember
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Designation { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Department { get; set; }

        [MaxLength(50)]
        public string? Batch { get; set; }

        [MaxLength(500)]
        public string? Photo { get; set; }

        [MaxLength(500)]
        public string? Facebook { get; set; }

        [MaxLength(500)]
        public string? LinkedIn { get; set; }

        [MaxLength(500)]
        public string? Bio { get; set; }

        public int DisplayOrder { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
