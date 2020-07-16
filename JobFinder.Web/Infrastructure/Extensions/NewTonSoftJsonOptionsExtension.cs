namespace JobFinder.Web.Infrastructure.Extensions
{
    using JobFinder.Data.Models.Enums;
    using JobFinder.Web.Infrastructure.JsonConverters;
    using Microsoft.AspNetCore.Mvc;

    public static class NewTonSoftJsonOptionsExtension
    {
        public static void Configure(this MvcNewtonsoftJsonOptions options)
        {
            options.SerializerSettings.Converters.Add(new EnumConverter<BusinessSector>());
            options.SerializerSettings.Converters.Add(new EnumConverter<EducationLevel>());
            options.SerializerSettings.Converters.Add(new EnumConverter<LanguageType>());
        }
    }
}
