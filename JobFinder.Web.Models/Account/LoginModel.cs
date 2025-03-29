using System.ComponentModel.DataAnnotations;
using static JobFinder.Common.Constants;
using static JobFinder.Common.MessageConstants;

namespace JobFinder.Web.Models.Account
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(PasswordMaxLength, ErrorMessage = ErrorModelPropertyMessage, MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
