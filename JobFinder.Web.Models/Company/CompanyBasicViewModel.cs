using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;

namespace JobFinder.Web.Models.Company
{
    public class CompanyBasicViewModel : IMapFrom<CompanyBasicDTO>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }
    }
}
