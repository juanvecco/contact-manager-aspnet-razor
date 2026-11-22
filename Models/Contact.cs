using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [MinLength(6)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{9}$")]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

    }
}
