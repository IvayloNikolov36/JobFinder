using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;

namespace JobFinder.Web.Models.Subscriptions.CompanySubscriptions
{
    public class CompanyJobAdsForSubscribersViewModel : IMapFrom<CompanyJobAdsForSubscribersDTO>
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogo { get; set; }

        public string JobAdIds { get; set; }

        public string Positions { get; set; }

        public string Locations { get; set; }

        public string JobCategories { get; set; }

        public string JobEngagements { get; set; }

        public string Salaries { get; set; }

        public string Subscribers { get; set; }
    }
}
