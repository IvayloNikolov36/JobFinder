using JobFinder.Data.Models;
using JobFinder.Services.Mappings;

namespace JobFinder.Web.Models.Company
{
    public class CompanyBasicViewModel : IMapFrom<CompanyEntity>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }
    }
}
