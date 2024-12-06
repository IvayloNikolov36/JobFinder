namespace JobFinder.Web.Infrastructure.Extensions
{
    using JobFinder.Data;
    using JobFinder.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;
    using static JobFinder.Web.Infrastructure.WebConstants;

    public static class AppBuilderExtension
    {
        private static readonly IdentityRole[] roles =
        {
            new IdentityRole(AdminRole),
            new IdentityRole(CompanyRole)
        };

        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            IServiceScopeFactory serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            IServiceScope scope = serviceFactory.CreateScope();

            using (scope)
            {
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();
                UserManager<UserEntity> userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<UserEntity>>();
                JobFinderDbContext db = scope.ServiceProvider
                    .GetRequiredService<JobFinderDbContext>();

                await CreateRoles(roleManager);

                // TODO: refactor
                // create admin
                await CreateUser(userManager, AdminUserName, AdminEmail, DefaultAdminPassword, AdminRole);
            }
        }

        private static async Task CreateUser(UserManager<UserEntity> userManager, string userName, string email, string defaultPassword, string role)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new UserEntity
                {
                    UserName = userName,
                    Email = email,
                    FirstName = AdminUserName,
                    MiddleName = AdminUserName,
                    LastName = AdminUserName
                };

                await userManager.CreateAsync(user, defaultPassword);
                await userManager.AddToRoleAsync(user, role);
            }
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}

