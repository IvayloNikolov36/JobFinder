using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.Web.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace JobFinder.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly UserManager<UserEntity> userManager;
    private readonly JobFinderDbContext dbContext;

    public AccountService(
        UserManager<UserEntity> userManager,
        JobFinderDbContext dbContext)
    {
        this.userManager = userManager;
        this.dbContext = dbContext;
    }

    public async Task<IdentityResult> ChangePassword(
        ChangePasswordModel model,
        string currentUserId)
    {
        UserEntity user = await this.dbContext.FindAsync<UserEntity>(currentUserId);

        IdentityResult result = await this.userManager
            .ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        return result;
    }
}
