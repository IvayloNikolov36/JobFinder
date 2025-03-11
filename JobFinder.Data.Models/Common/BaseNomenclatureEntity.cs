using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.Common
{
    public class BaseNomenclatureEntity<T> : BaseEntity<T>
    {
        [Required]
        public string Name { get; set; }
    }
}
