namespace JobFinder.Web
{
    using JobFinder.Data;
    using JobFinder.Data.Models;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Services.Mappings;
    using JobFinder.Services.Messages;
    using JobFinder.Web.Infrastructure.Extensions;
    using JobFinder.Web.Infrastructure.JsonConverters;
    using JobFinder.Web.Models.JobAds;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using Hangfire;
    using Hangfire.SqlServer;
    using System;
    using JobFinder.Web.Infrastructure.Filters;

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

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<JobFinderDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions()
                {
                    RequiredLength = 6,
                    RequiredUniqueChars = 1,
                    RequireLowercase = true,
                    RequireDigit = true,
                    RequireUppercase = true,
                    RequireNonAlphanumeric = false
                };

                options.SignIn.RequireConfirmedEmail = false;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtAudience"],
                    ValidIssuer = Configuration["JwtIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("JobFinderCORSPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader().WithMethods("GET", "POST", "DELETE", "PUT");
                    });
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new EnumConverter<BusinessSector>());
                options.SerializerSettings.Converters.Add(new EnumConverter<EducationLevel>());
                options.SerializerSettings.Converters.Add(new EnumConverter<LanguageType>());
            });

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), 
                    new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            //HangFire
            services.AddHangfireServer();

            services.AddDomainServices();
            services.AddTransient<IEmailSender, SendGridEmailSender>();

            // Add service filters.
            services.AddScoped<ValidateCvIdExistsServiceFilter>();
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

            app.UseRouting();

            app.UseCors("JobFinderCORSPolicy");

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
