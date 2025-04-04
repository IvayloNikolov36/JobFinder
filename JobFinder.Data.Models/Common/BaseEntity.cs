using JobFinder.Transfer.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.Data.Models.Common
{
    public class BaseEntity<T> : IAuditInfo<T>
    {
        [Key]
        public T Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [NotMapped]
        public Guid UniqueIdentificator { get; set; }
    }
}
