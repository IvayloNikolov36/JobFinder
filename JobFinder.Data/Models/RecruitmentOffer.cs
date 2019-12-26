using System;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models
{
    public class RecruitmentOffer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(90, MinimumLength = 6)]
        public string Position { get; set; }

        [Required]
        [MinLength(20)]
        public string Desription { get; set; }

        public DateTime Expiration { get; set; }

        public DateTime PostedOn { get; set; }

        public string PublisherId { get; set; }
        public User Publisher { get; set; }

    }
}
