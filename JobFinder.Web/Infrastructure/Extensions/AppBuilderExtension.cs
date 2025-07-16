using JobFinder.Data;
using JobFinder.Data.Models;
using Microsoft.AspNetCore.Identity;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Infrastructure.Extensions
{
    public static class AppBuilderExtension
    {
        private static readonly IdentityRole[] Roles =
        [
            new IdentityRole(AdminRole),
            new IdentityRole(CompanyRole),
            new IdentityRole(JobSeekerRole)
        ];

        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            IServiceScopeFactory serviceFactory = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>();

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
                await CreateUser(userManager, AdminUserName, AdminEmail, DefaultAdminPassword, AdminRole);
            }
        }

        private static async Task CreateUser(
            UserManager<UserEntity> userManager,
            string userName,
            string email,
            string defaultPassword,
            string role)
        {
            UserEntity user = await userManager.FindByNameAsync(userName);

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
            foreach (IdentityRole role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}

