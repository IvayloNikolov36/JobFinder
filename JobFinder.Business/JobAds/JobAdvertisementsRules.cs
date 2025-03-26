
using JobFinder.Common.Exceptions;
using System.Linq;
using static JobFinder.Common.MessageConstants;

namespace JobFinder.Business.JobAds
{
    public class JobAdvertisementsRules : IJobAdvertisementsRules
    {
        private const int DaysExpiration = 30;

        public void ValidateSalaryProperties(int? minSalary, int? maxSalary, int? currencyId)
        {
            if (maxSalary < minSalary)
            {
                throw new ActionableException(MaxSalaryRestriction);
            }

            bool noSalarySpecified = !minSalary.HasValue && !maxSalary.HasValue;

            if (currencyId.HasValue)
            {
                if (noSalarySpecified)
                {
                    throw new ActionableException(SpecifyMinAndMaxSalary);
                }             
                if (!minSalary.HasValue)
                {
                    throw new ActionableException(SpecifyMinSalary);
                }
                if (!maxSalary.HasValue)
                {
                    throw new ActionableException(SpecifyMaxSalary);
                }                             
            }
            else
            {
                if (minSalary.HasValue || maxSalary.HasValue)
                {
                    throw new ActionableException(SpecifyCurrency);
                }
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
                throw new ActionableException(string.Format(IntershipAppropriateEngagements, string.Join(", ", validJobEngagements)));
            }
        }

        public int GetDaysExpiration()
        {
            return DaysExpiration;
        }
    }
}
