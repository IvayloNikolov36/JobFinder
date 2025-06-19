using JobFinder.Business.JobAds;
using JobFinder.Common.Enums;
using JobFinder.Common.Exceptions;
using JobFinder.Transfer.DTOs;
using NUnit.Framework;
using static JobFinder.Common.MessageConstants;

namespace JobFinder.Business.UnitTests
{
    [TestFixture]
    public class JobAdsRulesTests
    {
        private JobAdsRules rules;

        [SetUp]
        public void Setup()
        {
            this.rules = new JobAdsRules();
        }

        [TestCase(5, 5, null, SpecifyCurrency)]
        [TestCase(5, 6, null, SpecifyCurrency)]
        [TestCase(4, 5, null, SpecifyCurrency)]
        [TestCase(4, null, null, SpecifyCurrency)]
        [TestCase(null, 5, null, SpecifyCurrency)]
        [TestCase(4, null, (int)CurrencyTypeEnum.BGN, SpecifyMaxSalary)]
        [TestCase(null, 4, (int)CurrencyTypeEnum.BGN, SpecifyMinSalary)]
        [TestCase(null, null, (int)CurrencyTypeEnum.BGN, SpecifyMinAndMaxSalary)]
        [TestCase(4, 3, (int)CurrencyTypeEnum.EUR, MaxSalaryRestriction)]
        public void ValidateSalaryProperties_ThrowsException(
            int? minSalary,
            int? maxSalary,
            int? currencyTypeId,
            string message)
        {
            SalaryPropertiesDTO salaryProperties = new SalaryPropertiesDTO
            {
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                HasCurrencyType = currencyTypeId.HasValue
            };

            Assert.That(() => this.rules.ValidateSalaryProperties(salaryProperties),
                Throws.TypeOf<ActionableException>().With.Message.EqualTo(message));
        }

        [TestCase(5, 5, (int)CurrencyTypeEnum.EUR)]
        [TestCase(5, 6, (int)CurrencyTypeEnum.EUR)]
        [TestCase(null, null, null)]
        public void ValidateSalaryProperties_DoesNot_ThrowException(
            int? minSalary,
            int? maxSalary,
            int? currencyTypeId)
        {
            SalaryPropertiesDTO salaryProperties = new SalaryPropertiesDTO
            {
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                HasCurrencyType = currencyTypeId.HasValue
            };

            Assert.DoesNotThrow(() => this.rules.ValidateSalaryProperties(salaryProperties));
        }

        [TestCase((int)JobEngagementEnum.FullTime)]
        [TestCase((int)JobEngagementEnum.PartTime)]
        [TestCase((int)JobEngagementEnum.Permanent)]
        [TestCase((int)JobEngagementEnum.Temporary)]
        [TestCase((int)JobEngagementEnum.SuitableForStudents)]
        [TestCase((int)JobEngagementEnum.SuitableForCandidatesWithNoExpirience)]
        public void ValidateIntership_DoesNotThrowException_WhenIntershipIsFalse_NoMAtterOfJobEngagementValue(int jobEngagementId)
        {
            Assert.DoesNotThrow(() => this.rules.ValidateIntership(false, jobEngagementId));
        }

        [TestCase((int)JobEngagementEnum.FullTime)]
        [TestCase((int)JobEngagementEnum.PartTime)]
        [TestCase((int)JobEngagementEnum.Temporary)]
        [TestCase((int)JobEngagementEnum.SuitableForStudents)]

        public void ValidateIntership_DoesNotThrowException_WhenIntershipIsTrue_WithSpecificJobEngagementValues(int jobEngagementId)
        {
            Assert.DoesNotThrow(() => this.rules.ValidateIntership(true, jobEngagementId));
        }

        [TestCase((int)JobEngagementEnum.Permanent)]
        [TestCase((int)JobEngagementEnum.SuitableForCandidatesWithNoExpirience)]
        public void ValidateIntership_TrowsException_WhenIntership_WithSpecificJobEngagementValues(int jobEngagementId)
        {
            Assert.That(() => this.rules.ValidateIntership(true, jobEngagementId),
                Throws.TypeOf<ActionableException>());
        }
    }
}
