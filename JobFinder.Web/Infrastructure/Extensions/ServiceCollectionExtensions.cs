using Hangfire;
using JobFinder.Business;
using JobFinder.DataAccess.Generic;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services;
using JobFinder.Services.Mappings;
using JobFinder.Services.Messages;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Infrastructure.Models;
using System.Reflection;

namespace JobFinder.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(
            this IServiceCollection services)
        {
            GetServiceTypes(GetAssemblyOf(typeof(IService)))
                .ForEach(s => services.AddScoped(s.Interface, s.Implementation));

            return services;
        }

        public static IServiceCollection AddBusinessServices(
            this IServiceCollection services)
        {
            GetServiceTypes(GetAssemblyOf(typeof(IBusinessRulesService)))
                .ForEach(s => services.AddSingleton(s.Interface, s.Implementation));

            return services;
        }

        public static IServiceCollection AddServiceFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidateCvIdBelongsToUser>();
            services.AddScoped<ValidateCompanyAccessingCVSentForOwnAd>();
            services.AddScoped<ValidateAnonymousProfileBelongsToUser>();
            services.AddScoped<ValidateJobAdBelongsToUser>();
            services.AddScoped<ValidateCompanyCanViewAnonymousProfile>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
            services.AddScoped<IEntityFrameworkUnitOfWork, EntityFrameworkUnitOfWork>();

            return services;
        }

        public static IServiceCollection AddAutomapperInstance(this IServiceCollection services)
        {
            services.AddSingleton(AutoMapperConfig.MapperInstance);

            return services;
        }

        public static IServiceCollection AddEmailSender(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, SendGridEmailSender>();

            return services;
        }

        public static IServiceCollection AddRecurringJobManager(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHangfire(globalConfig => globalConfig
                .Set(configuration.GetConnectionString("DefaultConnection")));

            services.AddHangfireServer();

            return services;
        }

        private static List<ServiceInfo> GetServiceTypes(Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(t => t.IsClass && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new ServiceInfo
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .ToList();
        }

        private static Assembly GetAssemblyOf(Type interfaceType)
        {
            return Assembly.GetAssembly(interfaceType);
        }
    }
}
