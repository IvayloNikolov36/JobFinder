namespace JobFinder.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterCompanyModel : RegisterModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 9)]
        public string Bulstat { get; set; }

        [Required]
        public string CompanyLogo { get; set; }
    }
}
