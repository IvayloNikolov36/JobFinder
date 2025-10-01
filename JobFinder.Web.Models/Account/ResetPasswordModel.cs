using System.ComponentModel.DataAnnotations;
using static JobFinder.Common.MessageConstants;

namespace JobFinder.Web.Models.Account;

public class ResetPasswordModel
{
    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(this.NewPassword), ErrorMessage = ErrorModelPropertyMessage)]
    public string ConfirmPassword { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Token { get; set; }
}
