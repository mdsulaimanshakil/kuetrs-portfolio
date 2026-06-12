using System.ComponentModel.DataAnnotations;

namespace KUETResearchSociety.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(200)]
        [Display(Name = "Your Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email.")]
        [MaxLength(300)]
        [Display(Name = "Your Email")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(300)]
        [Display(Name = "Subject")]
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [Display(Name = "Your Message")]
        public string Message { get; set; } = string.Empty;
    }
}
