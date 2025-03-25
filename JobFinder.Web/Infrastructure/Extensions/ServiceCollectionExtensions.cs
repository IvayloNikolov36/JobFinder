namespace JobFinder.Web.Infrastructure.Extensions
{
    using JobFinder.Business;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Data.Repositories;
    using JobFinder.Services;
    using JobFinder.Web.Infrastructure.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using System.Reflection;
    using JobFinder.Web.Infrastructure.Models;
    using System.Collections.Generic;
    using System;

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

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));

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
