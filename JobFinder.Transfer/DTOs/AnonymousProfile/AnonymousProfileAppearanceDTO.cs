namespace JobFinder.Transfer.DTOs.AnonymousProfile;

public class AnonymousProfileAppearanceDTO
{
    public int JobCategoryId { get; set; }

    public string PreferredPositions { get; set; }

    public IEnumerable<int> WorkplaceTypes { get; set; }

    public IEnumerable<int> JobEngagements { get; set; }

    public IEnumerable<int> SoftSkills { get; set; }

    public IEnumerable<int> ITAreas { get; set; }

    public IEnumerable<int> TechStacks { get; set; }

    public IEnumerable<int> Cities { get; set; }
}
