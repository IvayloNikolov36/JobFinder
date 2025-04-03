using JobFinder.Business.JobSubscriptions;
using JobFinder.Common.Exceptions;
using JobFinder.Transfer.DTOs;
using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
using NUnit.Framework;
using static JobFinder.Common.MessageConstants;

namespace JobFinder.Business.UnitTests
{
    [TestFixture]
    public class JobSubscriptionsRulesTests
    {
        private const int ValidRecurringTypeId = 1;
        private const int ValidJobCategoryId = 1;
        private const int ValidJobEngagementId = 1;
        private const int ValidLocationId = 1;
        private const string EmptySearchText = "";
        private const string WhiteSpaceSearchText = "     ";
        private const string ValidSearchText = "search";
        private const bool SpecifiedSalary = true;
        private const bool Intership = true;

        private IJobSubscriptionsRules jobSubscriptionsRules;

        [SetUp]
        public void Setup()
        {
            this.jobSubscriptionsRules = new JobSubscriptionsRules();
        }

        [TestCase(null, null, null, false, false, null)]
        [TestCase(null, null, null, false, false, EmptySearchText)]
        [TestCase(null, null, null, false, false, WhiteSpaceSearchText)]
        public void ValidateJobsSubscriptionProperties_ThrowsException_WhenNoCriteria_IsSpecified(
            int? categoryId,
            int? engagementId,
            int? locationId,
            bool intership,
            bool specifiedSalary,
            string searchText)
        {
            JobSubscriptionCriteriasDTO subscriptionCriterias = new()
            {
                RecurringTypeId = ValidRecurringTypeId,
                JobCategoryId = categoryId,
                JobEngagementId = engagementId,
                LocationId = locationId,
                Intership = intership,
                SpecifiedSalary = specifiedSalary,
                SearchTerm = searchText
            };

            Assert.That(() => this.jobSubscriptionsRules.ValidateJobsSubscriptionProperties(subscriptionCriterias),
                Throws.TypeOf<ActionableException>()
                .With
                .Message
                .EqualTo(NoSubscriptionCriterias));
        }

        [TestCase(ValidJobCategoryId, null, null, false, false, null)]
        [TestCase(null, ValidJobEngagementId, null, false, false, null)]
        [TestCase(null, null, ValidLocationId, false, false, null)]
        [TestCase(null, null, null, Intership, false, null)]
        [TestCase(null, null, null, false, SpecifiedSalary, null)]
        [TestCase(null, null, null, false, false, ValidSearchText)]
        public void ValidateJobsSubscriptionProperties_DoesNotThrowException_When_OneCriteria_IsSpecified(
            int? categoryId,
            int? engagementId,
            int? locationId,
            bool intership,
            bool specifiedSalary,
            string searchText)
        {
            JobSubscriptionCriteriasDTO subscriptionCriterias = new()
            {
                RecurringTypeId = ValidRecurringTypeId,
                JobCategoryId = categoryId,
                JobEngagementId = engagementId,
                LocationId = locationId,
                Intership = intership,
                SpecifiedSalary = specifiedSalary,
                SearchTerm = searchText
            };

            Assert.DoesNotThrow(() => this.jobSubscriptionsRules.ValidateJobsSubscriptionProperties(subscriptionCriterias));
        }

        [TestCase(ValidJobCategoryId, ValidJobCategoryId, null, false, false, null)]
        [TestCase(ValidJobCategoryId, ValidJobCategoryId, ValidLocationId, false, false, null)]
        [TestCase(ValidJobCategoryId, ValidJobCategoryId, ValidLocationId, Intership, false, null)]
        [TestCase(ValidJobCategoryId, ValidJobCategoryId, ValidLocationId, Intership, SpecifiedSalary, null)]
        public void ValidateJobsSubscriptionProperties_DoesNotThrowException_When_MoreThanOneCriteriaSpecified(
            int? categoryId,
            int? engagementId,
            int? locationId,
            bool intership,
            bool specifiedSalary,
            string searchText)
        {
            JobSubscriptionCriteriasDTO subscriptionCriterias = new()
            {
                RecurringTypeId = ValidRecurringTypeId,
                JobCategoryId = categoryId,
                JobEngagementId = engagementId,
                LocationId = locationId,
                Intership = intership,
                SpecifiedSalary = specifiedSalary,
                SearchTerm = searchText
            };

            Assert.DoesNotThrow(() => this.jobSubscriptionsRules.ValidateJobsSubscriptionProperties(subscriptionCriterias));
        }

        [TestCase(ValidJobCategoryId, ValidJobCategoryId, ValidLocationId, Intership, SpecifiedSalary, ValidSearchText)]
        public void ValidateJobsSubscriptionProperties_DoesNotThrowException_When_AllCriterias_AreSpecified(
            int? categoryId,
            int? engagementId,
            int? locationId,
            bool intership,
            bool specifiedSalary,
            string searchText)
        {
            JobSubscriptionCriteriasDTO subscriptionCriterias = new()
            {
                RecurringTypeId = ValidRecurringTypeId,
                JobCategoryId = categoryId,
                JobEngagementId = engagementId,
                LocationId = locationId,
                Intership = intership,
                SpecifiedSalary = specifiedSalary,
                SearchTerm = searchText
            };

            Assert.DoesNotThrow(() => this.jobSubscriptionsRules.ValidateJobsSubscriptionProperties(subscriptionCriterias));
        }
    }
}
