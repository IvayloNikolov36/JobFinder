namespace JobFinder.Web.Models.CurriculumVitae
{
    using System.ComponentModel.DataAnnotations;

    public class CVCreateInputModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string PictureUrl { get; set; }
    }
}
