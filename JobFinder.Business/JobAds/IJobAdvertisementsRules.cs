using JobFinder.Transfer.DTOs;

namespace JobFinder.Business.JobAds
{
    public interface IJobAdvertisementsRules
    {
        void ValidateSalaryProperties(SalaryPropertiesDTO salaryProperties);

        void ValidateIntership(bool intership, int jobEngagementId);

        int GetDaysExpiration();
    }
}
