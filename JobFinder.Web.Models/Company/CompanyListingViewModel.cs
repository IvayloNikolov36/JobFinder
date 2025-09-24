using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;
using JobFinder.Web.Models.Common;

namespace JobFinder.Web.Models.Company;

public class CompanyListingViewModel : BasicViewModel, IMapFrom<CompanyListingDTO>
{
    public string Logo { get; set; }

    public int Employees { get; set; }

    public int Ads { get; set; }
}
