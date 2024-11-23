namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;

    public class SkillsDrivingCategoryViewModel : IMapFrom<DrivingCategoryType>
    {
        public int Id { get; set; }

        public string Category { get; set; }
    }
}
