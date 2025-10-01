using JobFinder.Web.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace JobFinder.Services;

public interface IAccountService
{
    Task<IdentityResult> ChangePassword(ChangePasswordModel model, string currentUserId);

    Task SendPasswordResetLink(ForgottenPasswordModel model);

    Task<string> GeneratePasswordResetLink(ForgottenPasswordModel model);

    Task ResetPassword(ResetPasswordModel model, string currentUserId);
}
