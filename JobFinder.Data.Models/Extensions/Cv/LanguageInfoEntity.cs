using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.Data.Models.CV;

public partial class LanguageInfoEntity : IMapFrom<LanguageInfoEditDTO>,
    IMapTo<LanguageInfoDTO>,
    IMapTo<LanguageInfoEditDTO>
{

}
