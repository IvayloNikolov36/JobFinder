using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.AnonymousProfile;
using System.Collections.Generic;

namespace JobFinder.Web.Models.AnonymousProfile;

public class AnonymousProfileCreateViewModel : IMapTo<AnonymousProfileAppearanceCreateDTO>
{
    public IEnumerable<int> WorkExpiriencesInfo { get; set; }

    public IEnumerable<int> EducationsInfo { get; set; }

    public IEnumerable<int> LanguagesInfo { get; set; }

    public IEnumerable<int> CoursesInfo { get; set; }

    public int RemoteJobPreferenceId { get; set; }

    public int JobCategoryId { get; set; }

    public string PreferredPositions { get; set; }

    public IEnumerable<int> JobEngagements { get; set; }

    public IEnumerable<int> SoftSkills { get; set; }

    public IEnumerable<int> ITAreas { get; set; }

    public IEnumerable<int> TechStacks { get; set; }
}
