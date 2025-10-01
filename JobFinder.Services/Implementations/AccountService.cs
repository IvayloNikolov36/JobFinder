using JobFinder.Common.Exceptions;
using JobFinder.Data.Models;
using JobFinder.Services.Messaging;
using JobFinder.Services.Messaging.Models;
using JobFinder.Transfer.Constants;
using JobFinder.Transfer.Options;
using JobFinder.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Web;

namespace JobFinder.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly UserManager<UserEntity> userManager;
    private readonly IEmailSender emailSender;
    private readonly IConfiguration configuration;
    private readonly string requestUrl;

    public AccountService(
        UserManager<UserEntity> userManager,
        IEmailSender emailSender,
        IConfiguration configuration,
        IOptions<RequestUrlOptions> options)
    {
        this.userManager = userManager;
        this.emailSender = emailSender;
        this.configuration = configuration;
        this.requestUrl = options.Value.RequestBaseUrl;
    }

    public async Task<IdentityResult> ChangePassword(
        ChangePasswordModel model,
        string currentUserId)
    {
        UserEntity user = await this.userManager.FindByIdAsync(currentUserId);

        IdentityResult result = await this.userManager
            .ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        return result;
    }

    public async Task SendPasswordResetLink(ForgottenPasswordModel model)
    {
        string link = await this.GeneratePasswordResetLink(model);

        EmailProps props = new EmailProps
        {
            SenderEmail = this.configuration.GetSection("AppAccount:email").Value,
            Sender = this.configuration.GetSection("AppAccount:name").Value,
            RecipientEmail = model.Email,
            Subject = "Password Reset Link",
            HtmlContent = HtmlConstants.GetLink("Reset Password", link)
        };

        await this.emailSender.SendEmailAsync(props);
    }

    public async Task<string> GeneratePasswordResetLink(ForgottenPasswordModel model)
    {
        UserEntity user = await this.userManager.FindByEmailAsync(model.Email)
            ?? throw new ActionableException("Invalid email");

        string token = await this.userManager
            .GeneratePasswordResetTokenAsync(user)
            ?? throw new ActionableException("Unable to generate token for the link");

        string encodedToken = HttpUtility.UrlEncode(token);
        string callBackUrl = $"{this.requestUrl}/forgotten-password/reset-password?token={encodedToken}&email={model.Email}";

        return callBackUrl;
    }

    public async Task ResetPassword(ResetPasswordModel model, string currentUserId)
    {
        UserEntity user = await this.userManager.FindByIdAsync(currentUserId);

        bool isTokenValid = await this.userManager
            .VerifyUserTokenAsync(user, "Default", "PasswordReset", model.Token);

        if (!isTokenValid)
        {
            throw new ActionableException("The link for password reset is expired or invalid!");
        }

        IdentityResult resetResult = await this.userManager
            .ResetPasswordAsync(user, model.Token, model.NewPassword);

        if (!resetResult.Succeeded)
        {
            string errors = string
                .Join(Environment.NewLine, resetResult.Errors.Select(e => e.Description));

            throw new ActionableException($"Password Reset failed. {errors}");
        }
    }
}
