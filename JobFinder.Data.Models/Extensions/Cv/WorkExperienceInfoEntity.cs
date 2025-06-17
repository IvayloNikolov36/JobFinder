using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.Data.Models.Cv;

public partial class WorkExperienceInfoEntity : IMapFrom<WorkExperienceInfoEditDTO>,
    IMapTo<WorkExperienceInfoDTO>,
    IMapTo<WorkExperienceInfoEditDTO>,
    IMapFrom<WorkExperienceInfoInputDTO>
{

}
