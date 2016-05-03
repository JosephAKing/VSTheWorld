using System.ComponentModel.DataAnnotations;

namespace VSTheWorld.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Subject { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 5)]
        public string Message { get; set; }
    }
}
