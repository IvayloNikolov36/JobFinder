namespace JobFinder.Web.Models.JobAds
{
    using System.ComponentModel.DataAnnotations;

    public class JobAdEditModel
    {
        [Required]
        [StringLength(90, MinimumLength = 6)]
        public string Position { get; set; }

        [Required]
        [MinLength(20)]
        public string Desription { get; set; }

        public string PublisherId { get; set; }
    }
}
