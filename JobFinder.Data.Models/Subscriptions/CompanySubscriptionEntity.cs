namespace JobFinder.Data.Models.Subscriptions
{
    using JobFinder.Data.Models.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CompanySubscriptionEntity : IAuditInfo
    {
        [Required]
        public string UserId { get; set; }
        public UserEntity User { get; set; }

        public int CompanyId { get; set; }
        public CompanyEntity Company { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
