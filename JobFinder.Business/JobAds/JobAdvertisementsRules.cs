
using JobFinder.Common.Exceptions;

namespace JobFinder.Business.JobAds
{
    public class JobAdvertisementsRules : IJobAdvertisementsRules
    {
        private const int DaysExpiration = 30;

        public void ValidateSalaryProperties(int? minSalary, int? maxSalary, int? currencyId)
        {
            if (maxSalary < minSalary)
            {
                throw new ActionableException("Max Salary must be equal to or grater than Min Salary!");
            }

            bool isIncompleteSalaryDiapason = (minSalary.HasValue && !maxSalary.HasValue)
                || (!minSalary.HasValue && maxSalary.HasValue);

            if (isIncompleteSalaryDiapason)
            {
                throw new ActionableException("You have to specify both min and max salary!");
            }

            bool hasSalaryDiapason = minSalary.HasValue && maxSalary.HasValue;

            if (hasSalaryDiapason && !currencyId.HasValue)
            {
                throw new ActionableException("You have to specify currency type!");
            }

            if (!hasSalaryDiapason && currencyId.HasValue)
            {
                throw new ActionableException("You specified currency but forgot to specify min and max salary.");
            }
        }

        public void ValidateIntership(bool intership, int jobEngagementId)
        {
            if (!intership)
            {
                return;
            }

            // TODO: think about a different approach - generating a enum from the db table and using it here

            int[] validJobEngagementIds = [1, 2, 4, 5];
            string[] validJobEngagements = ["Full time", "Part time", "Temporary", "Suitable for students"];

            if (intership && !validJobEngagementIds.Contains(jobEngagementId))
            {
                throw new ActionableException(
                    $"When selecting Intership, you have to select one of these Job Engagements: {string.Join(", ", validJobEngagements)}");
            }
        }

        public int GetDaysExpiration()
        {
            return DaysExpiration;
        }
    }
}
