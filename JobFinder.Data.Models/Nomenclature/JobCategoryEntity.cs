using JobFinder.Data.Models.Common;

namespace JobFinder.Data.Models.Nomenclature
{
    public class JobCategoryEntity : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
