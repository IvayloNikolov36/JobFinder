using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;

namespace JobFinder.Data.Models.Cv;

public partial class CvPreviewRequestEntity : IMapFrom<CvPreviewRequestDTO>,
    IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<CvPreviewRequestEntity, CvPreviewRequestListingDTO>()
            .ForMember(dto => dto.Company, o => o.MapFrom(e => e.JobAd.Publisher));

        configuration.CreateMap<CvPreviewRequestEntity, CompanyCvPreviewRequestListingDTO>()
            .ForMember(dto => dto.CvId, o => o.MapFrom(e => e.AcceptedDate.HasValue
                ? e.CvId
                : null));
    }
}
