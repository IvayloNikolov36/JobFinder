namespace JobFinder.Web.Infrastructure.Extensions
{
    using JobFinder.Data;
    using JobFinder.Data.Models;
    using JobFinder.Data.Models.CV;
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
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();

            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var db = scope.ServiceProvider.GetRequiredService<JobFinderDbContext>();

                await CreateRoles(roleManager);

                // TODO: refactor
                //create admin
                await CreateUser(userManager, AdminUserName, AdminEmail, DefaultAdminPassword, AdminRole);

                await CreateDrivingLicenseCategories(db);
            }
        }

        private static async Task CreateUser(UserManager<User> userManager, string userName, string email, string defaultPassword, string role)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new User
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

        // TODO: remove and create a seed
        private static async Task CreateDrivingLicenseCategories(JobFinderDbContext db)
        {
            string[] categories = new string[] { "A", "B", "C", "D", "BE", "CE", "DE", "T tb", "T tm", "T ct", "M" };

            foreach (string category in categories)
            {
                var categoryType = new DrivingCategoryType
                {
                    Category = category
                };

                await db.AddAsync(categoryType);
            }

            await db.SaveChangesAsync();
        }

    }
}

