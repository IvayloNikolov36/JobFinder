namespace JobFinder.Data.Models.CV
{
    public class DrivingCategory
    {
        public int Id { get; set; }

        public int DrivingCategoryTypeId { get; set; } //FK

        public int SkillId { get; set; }

        public Skill Skill { get; set; }
    }
}
