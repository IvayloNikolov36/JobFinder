using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Web.Models.Company;

namespace JobFinder.Web.Models.Subscriptions.CompanySubscriptions
{
    public class CompanyJobAdsForSubscribersViewModel : IMapFrom<CompanyJobAdsForSubscribersDTO>
    {
        public CompanyBasicDetailsViewModel Company {  get; set; }

        public string JobAdIds { get; set; }

        public string Positions { get; set; }

        public string Locations { get; set; }

        public string JobCategories { get; set; }

        public string JobEngagements { get; set; }

        public string Salaries { get; set; }

        public string Subscribers { get; set; }
    }
}
