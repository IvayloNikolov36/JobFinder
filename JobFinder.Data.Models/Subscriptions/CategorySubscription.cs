﻿namespace JobFinder.Data.Models.Subscriptions
{
    using System.ComponentModel.DataAnnotations;

    public class JobCategorySubscription
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public int JobCategoryId { get; set; }

        public JobCategory JobCategory { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
