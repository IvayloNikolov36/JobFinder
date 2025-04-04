using JobFinder.Transfer.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.Subscriptions
{
    public class CompanySubscriptionEntity : IAudit
    {
        [Required]
        public string UserId { get; set; }
        public UserEntity User { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public CompanyEntity Company { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
