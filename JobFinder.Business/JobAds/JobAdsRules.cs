
using JobFinder.Common.Exceptions;
using JobFinder.Transfer.DTOs;
using System.Linq;
using static JobFinder.Common.MessageConstants;

namespace JobFinder.Business.JobAds
{
    public class JobAdsRules : IJobAdsRules
    {
        private const int DaysExpiration = 30;

        public void ValidateSalaryProperties(SalaryPropertiesDTO salaryProperties)
        {
            if (salaryProperties.MaxSalary < salaryProperties.MinSalary)
            {
                throw new ActionableException(MaxSalaryRestriction);
            }

            bool noSalarySpecified = !salaryProperties.MinSalary.HasValue && !salaryProperties.MaxSalary.HasValue;

            if (salaryProperties.HasCurrencyType)
            {
                if (noSalarySpecified)
                {
                    throw new ActionableException(SpecifyMinAndMaxSalary);
                }             
                if (!salaryProperties.MinSalary.HasValue)
                {
                    throw new ActionableException(SpecifyMinSalary);
                }
                if (!salaryProperties.MaxSalary.HasValue)
                {
                    throw new ActionableException(SpecifyMaxSalary);
                }                             
            }
            else
            {
                if (salaryProperties.MinSalary.HasValue || salaryProperties.MaxSalary.HasValue)
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

        public string GenerateSalaryText(int? minSalary, int? maxSalary, string currencyName)
        {
            if (minSalary.HasValue && maxSalary.HasValue && currencyName is not null)
            {
                return  $"{minSalary}-{maxSalary} {currencyName}";
            }

            return string.Empty;
        }
    }
}
