using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions
{
    public class JobSubscriptionCriteriasViewModel
    {
        [Range(1, int.MaxValue)]
        public int? JobCategoryId { get; set; }

        [Range(1, int.MaxValue)]
        public int? LocationId { get; set; }
    }
}
