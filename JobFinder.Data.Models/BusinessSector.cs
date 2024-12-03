using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System.Collections.Generic;

namespace JobFinder.Data.Models
{
    public class BusinessSector : BaseModel<int>
    {
        public BusinessSector()
        {
            this.WorkExperiences = new List<WorkExperience>();
        }

        public string Name { get; set; }

        public ICollection<WorkExperience> WorkExperiences { get; set; }
    }
}
