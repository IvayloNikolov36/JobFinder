namespace JobFinder.Data.Models.Subscriptions
{
    using System.ComponentModel.DataAnnotations;

    public class CompanySubscription
    {
        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
