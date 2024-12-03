using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System.Collections.Generic;

namespace JobFinder.Data.Models
{
    public class EducationLevel : BaseModel<int>
    {
        public EducationLevel()
        {
            this.Educations = new List<Education>();
        }

        public string Name { get; set; }

        public ICollection<Education> Educations { get; set; }
    }
}
