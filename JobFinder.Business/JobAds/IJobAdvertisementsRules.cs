namespace JobFinder.Business.JobAds
{
    public interface IJobAdvertisementsRules
    {
        void ValidateSalaryProperties(int? minSalary, int? maxSalary, int? currencyId);

        void ValidateIntership(bool intership, int jobEngagementId);

        int GetDaysExpiration();
    }
}
