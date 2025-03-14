namespace JobFinder.Data.Models.Subscriptions
{
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Nomenclature;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CompanySubscriptionEntity : IAuditInfo
    {
        [Required]
        public string UserId { get; set; }
        public UserEntity User { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public CompanyEntity Company { get; set; }

        [Required]
        public int RecuringTypeId { get; set; }
        public ReccuringTypeEntity RecuringType { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
