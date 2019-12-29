using System;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.RecruitmentOffers
{
    public class RecruitmentOfferBindingModel
    {
        [Required]
        [StringLength(90, MinimumLength = 6)]
        public string Position { get; set; }

        [Required]
        [MinLength(20)]
        public string Desription { get; set; }

        public int DaysActive { get; set; }

        public string PublisherId { get; set; }
    }
}
