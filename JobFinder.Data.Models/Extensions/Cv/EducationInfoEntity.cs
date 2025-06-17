using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.Data.Models.Cv;

public partial class EducationInfoEntity : IMapFrom<EducationInfoEditDTO>,
    IMapTo<EducationInfoDTO>,
    IMapTo<EducationInfoEditDTO>,
    IMapFrom<EducationInfoInputDTO>
{

}
