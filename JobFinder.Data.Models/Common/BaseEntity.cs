namespace JobFinder.Data.Models.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity<T> : IAuditInfo
    {
        [Key]
        public T Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
