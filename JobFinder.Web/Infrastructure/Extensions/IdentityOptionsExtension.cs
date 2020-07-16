namespace JobFinder.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Identity;

    public static class IdentityOptionsExtension
    {
        public static void Configure(this IdentityOptions identityOptions)
        {
            var passwordOptions = new PasswordOptions()
            {
                RequiredLength = 6,
                RequiredUniqueChars = 1,
                RequireLowercase = true,
                RequireDigit = true,
                RequireUppercase = true,
                RequireNonAlphanumeric = false
            };

            identityOptions.Password = passwordOptions;
            identityOptions.SignIn.RequireConfirmedEmail = false;
        }
    }
}
