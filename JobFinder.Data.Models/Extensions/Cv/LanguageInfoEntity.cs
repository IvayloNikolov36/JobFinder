using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.Data.Models.Cv;

public partial class LanguageInfoEntity : IMapFrom<LanguageInfoEditDTO>,
    IMapFrom<LanguageInfoInputDTO>,
    IMapTo<LanguageInfoDTO>,
    IMapTo<LanguageInfoEditDTO>
{

}
