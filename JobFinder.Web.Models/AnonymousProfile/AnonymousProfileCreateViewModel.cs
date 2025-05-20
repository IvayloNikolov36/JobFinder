using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.AnonymousProfile;

public class AnonymousProfileCreateViewModel
{
    [Required]
    public string CurriculumVitaeId { get; set; }

    public IEnumerable<int> WorkExpiriencesInfo { get; set; }

    public IEnumerable<int> EducationsInfo { get; set; }

    public IEnumerable<int> LanguagesInfo { get; set; }

    public IEnumerable<int> CoursesInfo { get; set; }
}
