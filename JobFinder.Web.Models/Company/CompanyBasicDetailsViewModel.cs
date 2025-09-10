using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;
using JobFinder.Web.Models.Common;

namespace JobFinder.Web.Models.Company
{
    public class CompanyBasicDetailsViewModel : BasicViewModel, IMapFrom<CompanyBasicDetailsDTO>
    {
        public string Logo { get; set; }
    }
}
