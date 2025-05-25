using System.Collections.Generic;

namespace JobFinder.Web.Models.AnonymousProfile;

public class AnonymousProfileCreateViewModel
{
    public IEnumerable<int> WorkExpiriencesInfo { get; set; }

    public IEnumerable<int> EducationsInfo { get; set; }

    public IEnumerable<int> LanguagesInfo { get; set; }

    public IEnumerable<int> CoursesInfo { get; set; }
}
