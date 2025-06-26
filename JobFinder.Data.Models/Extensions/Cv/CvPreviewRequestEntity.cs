using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.Data.Models.Cv;

public partial class CvPreviewRequestEntity : IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<CvPreviewRequestDTO, CvPreviewRequestEntity>()
            .ForMember(e => e.AnonymousProfilePreviewedId, o => o.MapFrom(dto => dto.AnonymousProfileId));
    }
}
