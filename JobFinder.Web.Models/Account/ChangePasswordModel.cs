using System.ComponentModel.DataAnnotations;
using static JobFinder.Common.Constants;
using static JobFinder.Common.MessageConstants;

namespace JobFinder.Web.Models.Account
{
    public class ChangePasswordModel
    {
        [Required]
        [StringLength(
            PasswordMaxLength,
            ErrorMessage = ErrorModelPropertyMessage,
            MinimumLength = PasswordMinLength)
        ]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(
            PasswordMaxLength,
            ErrorMessage = ErrorModelPropertyMessage,
            MinimumLength = PasswordMinLength)
        ]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(this.NewPassword), ErrorMessage = ErrorModelPropertyMessage)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
