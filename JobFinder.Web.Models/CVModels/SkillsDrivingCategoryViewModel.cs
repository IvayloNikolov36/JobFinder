namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.Nomenclature;
    using JobFinder.Services.Mappings;

    public class SkillsDrivingCategoryViewModel : IMapFrom<DrivingCategoryEntity>
    {
        public int Id { get; set; }

        public string Category { get; set; }
    }
}
