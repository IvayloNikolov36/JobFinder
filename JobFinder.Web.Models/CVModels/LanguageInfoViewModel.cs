using JobFinder.Data.Models.Cv;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.Common;

namespace JobFinder.Web.Models.CvModels;

public class LanguageInfoViewModel : IMapFrom<LanguageInfoEntity>, IMapFrom<LanguageInfoDTO>
{
    public int Id { get; set; }

    public bool? IncludeInAnonymousProfile { get; set; }

    public BasicViewModel LanguageType { get; set; }

    public BasicViewModel ComprehensionLevel { get; set; }

    public BasicViewModel SpeakingLevel { get; set; }

    public BasicViewModel WritingLevel { get; set; }
}
