using Hangfire;
using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using JobFinder.Services.Messages;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.JobAds;
using JobFinder.Web.Infrastructure;
using JobFinder.Web.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using JobFinder.Data.Models.ViewsModels;

namespace JobFinder.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AutoMapperConfig.RegisterMappings(this.GetAssembliesForAutomapper());

            services.AddSingleton(AutoMapperConfig.MapperInstance);

            services.AddDbContext<JobFinderDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<UserEntity, IdentityRole>().AddEntityFrameworkStores<JobFinderDbContext>();

            services.Configure<IdentityOptions>(options => options.Configure());

            services
                .AddAuthentication(options => options.Configure())
                .AddJwtBearer(options => options.Configure(
                    validAudience: Configuration["JwtAudience"],
                    validIssuer: Configuration["JwtIssuer"],
                    jwtSecurityKey: Configuration["JwtSecurityKey"])
                );

            services.AddResponseCaching();

            services.AddCors(options => options.Configure());

            services.AddControllers();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo() { Title = "JobFinder API", Version = "v1" });
            });

            services.AddHangfire(configuration => configuration
                .Set(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();

            services.AddServiceFilters()
                .AddBusinessServices()
                .AddRepositories()
                .AddDomainServices();

            services.AddTransient<IEmailSender, SendGridEmailSender>();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                using (IServiceScope scope = app.ApplicationServices.CreateScope())
                {
                    using JobFinderDbContext context = scope.ServiceProvider.GetService<JobFinderDbContext>();
                    context.Database.EnsureCreated();
                }

                app.SeedDatabase();

                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "JobFinder API");
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors(WebConstants.CorsPolicyName);
            app.UseResponseCaching();

            app.UseAuthentication();
            app.UseAuthorization();

            this.ConfigureHangfire(app, recurringJobManager, serviceProvider);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureHangfire(
            IApplicationBuilder app,
            IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            app.UseHangfireDashboard();
            recurringJobManager.RegisterDeactiveJobAdvertisements(serviceProvider);
            recurringJobManager.RegisterSendLatestCompanyJobAdvertisements(serviceProvider);
            recurringJobManager.RegisterSendingJobAdvertisementsBySubscriptions(serviceProvider);
        }

        private Assembly[] GetAssembliesForAutomapper()
            => [typeof(JobAdCreateViewModel).Assembly,
                typeof(UserEntity).Assembly,
                typeof(CompanyJobAdsForSubscribersViewData).Assembly];
    }
}
