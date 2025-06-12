using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.AnonymousProfile;
using System.Collections.Generic;

namespace JobFinder.Web.Models.AnonymousProfile;

public class AnonymousProfileAppearanceCriteriaViewModel :
    IMapTo<AnonymousProfileAppearanceDTO>,
    IMapFrom<AnonymousProfileAppearanceDTO>
{
    public int JobCategoryId { get; set; }

    public IEnumerable<int> JobEngagements { get; set; }

    public string PreferredPositions { get; set; }

    public IEnumerable<int> SoftSkills { get; set; }

    public IEnumerable<int> ITAreas { get; set; }

    public IEnumerable<int> TechStacks { get; set; }

    public IEnumerable<int> WorkplaceTypes { get; set; }
}
