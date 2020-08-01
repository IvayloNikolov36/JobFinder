namespace JobFinder.Web
{
    using JobFinder.Data;
    using JobFinder.Data.Models;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Infrastructure.Extensions;
    using JobFinder.Web.Models.JobAds;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Hangfire;
    using System;
    using JobFinder.Web.Infrastructure.Filters;
    using static JobFinder.Web.Infrastructure.WebConstants;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Data.Repositories;
    using JobFinder.Services.Messages;
    using Microsoft.AspNetCore.Mvc;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AutoMapperConfig
                .RegisterMappings(typeof(JobAdBindingModel).Assembly);

            services.AddDbContext<JobFinderDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<JobFinderDbContext>();

            services.Configure<IdentityOptions>(options => options.Configure());

            services.AddAuthentication(options => options.Configure())
            .AddJwtBearer(options => options.Configure(
                validAudience: Configuration["JwtAudience"],
                validIssuer: Configuration["JwtIssuer"],
                jwtSecurityKey: Configuration["JwtSecurityKey"])
            );

            services.AddResponseCaching();
            services.AddHttpCacheHeaders();

            services.AddCors(options => options.Configure());

            services.AddControllers(config =>
                config.CacheProfiles.Add(
                    "JobAdsCashProfile",
                    new CacheProfile
                    {
                        Duration = 120,
                        Location = ResponseCacheLocation.Any
                    })
                ).AddNewtonsoftJson(options => options.Configure());

            //HangFire
            services.AddHangfire(configuration => configuration
                .Set(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();

            // Add service filters.
            services.AddScoped<ValidateCvIdExistsServiceFilter>();

            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
            services.AddDomainServices();

            services.AddTransient<IEmailSender, SendGridEmailSender>();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IRecurringJobManager reccuringJobManager,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.SeedDatabase();
            }

            app.UseHttpsRedirection();
            app.UseCors(CorsPolicyName); //UserCors must be before UseResponseCashing
            app.UseResponseCaching();
            app.UseHttpCacheHeaders();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //HangFire
            app.UseHangfireDashboard();
            reccuringJobManager.AddOrUpdate(
                "sendingLatestJobAdsByCompany",
                () => serviceProvider.GetService<IDataSender>().SendLatestJobAdsBySubscribedCompanies(),
                "0 0 */1 * *");

            reccuringJobManager.AddOrUpdate(
                "sendingLatestJobAdsByCategoryAndLocation",
                () => serviceProvider.GetService<IDataSender>()
                    .SendLatestJobAdsBySubscribedCategoriesAndLocations(),
                "0 0 */1 * *");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
