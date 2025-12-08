using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;
using System.ComponentModel.DataAnnotations;
using static JobFinder.Transfer.Constants.ValidationConstants;

namespace JobFinder.Web.Models.Company
{
    public class CompanyEditViewModel : IMapTo<CompanyEditDTO>
    {
        [StringLength(CompanyNameMaxLen, MinimumLength = CompanyNameMinLen)]
        public string Name { get; set; }

        [StringLength(CompanyBulstatMaxLen, MinimumLength = CompanyBulstatMinLen)]
        public string Bulstat { get; set; }

        [Range(0, int.MaxValue)]
        public int Employees { get; set; }
    }
}
