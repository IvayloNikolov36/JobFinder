using JobFinder.Transfer.DTOs;

namespace JobFinder.Business.JobAds
{
    public interface IJobAdsRules
    {
        void ValidateSalaryProperties(SalaryPropertiesDTO salaryProperties);

        void ValidateIntership(bool intership, int jobEngagementId);

        int GetDaysExpiration();

        string GenerateSalaryText(int? minSalary, int? maxSalary, string currencyName);
    }
}
