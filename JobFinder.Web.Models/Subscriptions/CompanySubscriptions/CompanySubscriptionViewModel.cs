using JobFinder.Data.Models.Subscriptions;
using JobFinder.Services.Mappings;

namespace JobFinder.Web.Models.Subscriptions.CompanySubscriptions
{
    public class CompanySubscriptionViewModel : IMapFrom<CompanySubscriptionEntity>
    {
        public int CompanyId { get; set; }

        public string CompanyLogo { get; set; }

        public string CompanyName { get; set; }
    }
}
