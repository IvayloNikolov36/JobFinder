using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.Data.Models.Cv;

public partial class CvPreviewRequestEntity : IMapFrom<CvPreviewRequestDTO>,
    IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<CvPreviewRequestEntity, CvPreviewRequestListingDTO>()
            .ForMember(dto => dto.CompanyName, o => o.MapFrom(e => e.JobAd.Publisher.Name))
            .ForMember(dto => dto.CompanyLogoUrl, o => o.MapFrom(e => e.JobAd.Publisher.Logo))
            .ForMember(dto => dto.Position, o => o.MapFrom(e => e.JobAd.Position));
    }
}
