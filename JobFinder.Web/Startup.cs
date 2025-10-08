using Hangfire;
using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.Options;
using JobFinder.Web.Infrastructure;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Infrastructure.Middlewares;
using JobFinder.Web.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;

namespace JobFinder.Web
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<JobFinderDbContext>(options => options.Configure(this.configuration));

            services.AddIdentity<UserEntity, IdentityRole>()
                .AddEntityFrameworkStores<JobFinderDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<UserEntity>>(TokenOptions.DefaultProvider);

            services.Configure<IdentityOptions>(options => options.Configure());

            services
                .AddAuthentication(options => options.Configure())
                .AddJwtBearer(options => options.Configure(this.configuration));

            services
                .Configure<RequestUrlOptions>(options => configuration
                    .GetSection("RequestUrl").Bind(options));

            services.AddResponseCaching();
            services.AddDistributedMemoryCache();

            services.AddCors(options => options.Configure());

            services.AddControllers();

            services.AddSwaggerGen(options => options.Configure());

            AutoMapperConfig.RegisterMappings(AssembliesProvider.AutomapperAssemblies());

            services.AddCloudinary(this.configuration);
           
            services.AddServiceFilters()
                .AddBusinessServices()
                .AddRepositories()
                .AddDomainServices()
                .AddAutomapperInstance()
                .AddEmailSender()
                .AddRecurringJobManager(this.configuration);
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
                    using JobFinderDbContext context = scope.ServiceProvider
                        .GetService<JobFinderDbContext>();

                    context.Database.EnsureCreated();
                }

                app.SeedDatabase();

                app.UseSwagger();
                app.UseSwaggerUI(options => options.Configure());
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors(WebConstants.CorsPolicyName);
            app.UseResponseCaching();

            app.UseAuthentication();
            app.UseAuthorization();

            app.ConfigureHangfire(recurringJobManager, serviceProvider);

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
