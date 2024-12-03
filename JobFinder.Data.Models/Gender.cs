using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System.Collections.Generic;

namespace JobFinder.Data.Models
{
    public class Gender : BaseModel<int>
    {
        public Gender()
        {
            this.PersonalInfos = new List<PersonalInfo>();
        }

        public string Name { get; set; }

        public ICollection<PersonalInfo> PersonalInfos { get; set; }
    }
}
