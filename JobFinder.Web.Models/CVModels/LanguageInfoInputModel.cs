using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.Web.Models.CvModels
{
    public class LanguageInfoInputModel : IMapTo<LanguageInfoInputDTO>
    {
        public int LanguageTypeId { get; set; }

        public int ComprehensionLevelId { get; set; }

        public int SpeakingLevelId { get; set; }

        public int WritingLevelId { get; set; }
    }
}
