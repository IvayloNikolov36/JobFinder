using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;

namespace JobFinder.Web.Models.Company
{
    public class CompanyDetailsUserViewModel : IMapFrom<CompanyDetailsUserDTO>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public int ActiveAdsCount { get; set; }

        public bool HasSubscription { get; set; }
    }
}
