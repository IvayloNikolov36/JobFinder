using JobFinder.Data.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models
{
    public class LanguageType : BaseModel<int>
    {
        public LanguageType()
        {
            this.LanguageTypes = new List<LanguageType>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<LanguageType> LanguageTypes { get; set; }
    }
}
