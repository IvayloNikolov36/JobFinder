using JobFinder.Data.Models.Enums;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.Business.JobAds
{
    public interface IJobAdsRules
    {
        void ValidateSalaryProperties(SalaryPropertiesDTO salaryProperties);

        void ValidateIntership(bool intership, int jobEngagementId);

        int GetDaysExpiration();

        string GenerateSalaryText(int? minSalary, int? maxSalary, string currencyName);

        void ValidateJobCategoryAndRelatedData(JobAdCategoryDTO adCategoryDto);

        bool CanActivateAd(LifecycleStatusEnum status);
    }
}
