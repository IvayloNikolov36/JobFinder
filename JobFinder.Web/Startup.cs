namespace JobFinder.Web
{
    using Hangfire;
    using JobFinder.Data;
    using JobFinder.Data.Models;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Infrastructure.Extensions;
    using JobFinder.Web.Models.JobAds;
    using JobFinder.Web.Infrastructure.Filters;
    using JobFinder.Data.Repositories;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Web.Infrastructure;
    using JobFinder.Web.Infrastructure.Middlewares;
    using JobFinder.Services.Messages;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using System;
    using System.Linq;
    using JobFinder.Services;
    using JobFinder.Web.Models.Common;
    using System.Collections.Generic;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AutoMapperConfig
                .RegisterMappings(typeof(JobAdCreateModel).Assembly);

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
            services.AddHttpCacheHeaders();

            services.AddCors(options => options.Configure());

            services.AddControllers();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo() { Title = "JobFinder API", Version = "v1" });
            });

            services.AddHangfire(configuration => configuration
                .Set(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();

            // Service Filters
            services.AddScoped<ValidateCvIdBelongsToUser>();
            services.AddScoped<ValidateCompanyAccessingCVSentForOwnAd>();

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
            app.UseAuthentication();
            app.UseAuthorization();

            this.ConfigureHangfire(app, reccuringJobManager, serviceProvider);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureHangfire(
            IApplicationBuilder app,
            IRecurringJobManager reccuringJobManager,
            IServiceProvider serviceProvider)
        {
            app.UseHangfireDashboard();

            string dailyCronExpression = "0 0 * * *";

            reccuringJobManager.AddOrUpdate(
                $"sending_Latest_CompanyJobAds",
                () => serviceProvider.GetService<IDataSender>().SendLatestJobAdsForCompanySubscriptions(),
                dailyCronExpression);

            string everySundayCronExpression = "0 0 * * SUN";
            string firstDayOfTheMonthCronExpression = "0 0 1 * *";

            string[] cronExpressions = [dailyCronExpression, everySundayCronExpression, firstDayOfTheMonthCronExpression];

            BasicViewModel[] recurringTypes = serviceProvider.GetService<INomenclatureService>().GetRecurringTypesSync().ToArray();

            int index = 0;
            foreach (string cronExpression in cronExpressions)
            {
                BasicViewModel recurringType = recurringTypes[index++];

                reccuringJobManager.AddOrUpdate(
                    $"sending_{recurringType.Name}_JobAdsByCriterias",
                    () => serviceProvider.GetService<IDataSender>().SendLatestJobAdsForJobSubscriptions(recurringType.Id),
                    cronExpression);
            }
        }
    }
}
