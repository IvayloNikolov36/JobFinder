using JobFinder.Data.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.Nomenclature
{
    public class LanguageTypeEntity : BaseEntity<int>
    {
        public LanguageTypeEntity()
        {
            this.LanguageTypes = new List<LanguageTypeEntity>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<LanguageTypeEntity> LanguageTypes { get; set; }
    }
}
