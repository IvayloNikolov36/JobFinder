using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;

namespace JobFinder.Web.Models.Subscriptions.CompanySubscriptions
{
    public class CompanySubscriptionViewModel : IMapFrom<CompanySubscriptionDTO>
    {
        public int CompanyId { get; set; }

        public string CompanyLogo { get; set; }

        public string CompanyName { get; set; }
    }
}
