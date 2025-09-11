using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.Company;

namespace JobFinder.Data.Models.ViewsModels
{
    public class CompanyJobAdsForSubscribersViewData : IHaveCustomMappings
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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<CompanyJobAdsForSubscribersViewData, CompanyJobAdsForSubscribersDTO>()
                .ForMember(dto => dto.Company, o => o.MapFrom(vd => new CompanyBasicDetailsDTO
                {
                    Id = vd.CompanyId,
                    Name = vd.CompanyName,
                    Logo = vd.CompanyLogo
                }));
        }
    }
}
