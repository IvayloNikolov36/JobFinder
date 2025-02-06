using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions
{
    public class JobSubscriptionCriteriasViewModel
    {
        [Range(1, int.MaxValue)]
        public int? JobCategoryId { get; set; }

        public string Location { get; set; }
    }
}
