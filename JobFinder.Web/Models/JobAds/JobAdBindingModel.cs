using System;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.JobAds
{
    public class JobAdBindingModel
    {
        [Required]
        [StringLength(90, MinimumLength = 6)]
        public string Position { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        public int DaysActive { get; set; }
    }
}
