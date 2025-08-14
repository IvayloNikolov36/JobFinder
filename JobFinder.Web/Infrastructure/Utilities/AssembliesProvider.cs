using JobFinder.Data.Models;
using JobFinder.Data.Models.ViewsModels;
using JobFinder.Web.Models.JobAds;
using System.Reflection;

namespace JobFinder.Web.Infrastructure.Utilities
{
    public static class AssembliesProvider
    {
        public static Assembly[] AutomapperAssemblies() =>
            [
                typeof(JobAdCreateViewModel).Assembly,
                typeof(UserEntity).Assembly,
                typeof(CompanyJobAdsForSubscribersViewData).Assembly
            ];
    }
}
