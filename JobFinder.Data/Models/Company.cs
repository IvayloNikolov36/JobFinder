using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyLogo { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 9)]
        public string Bulstat { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
