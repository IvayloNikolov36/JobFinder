using JobFinder.Data.Models.Nomenclature;
using JobFinder.Services.Mappings;

namespace JobFinder.Web.Models.CvModels;

public class SkillsDrivingCategoryViewModel : IMapFrom<DrivingCategoryEntity>
{
    public int Id { get; set; }

    public string Category { get; set; }
}
