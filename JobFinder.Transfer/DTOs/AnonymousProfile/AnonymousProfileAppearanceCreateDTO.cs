namespace JobFinder.Transfer.DTOs.AnonymousProfile;

public class AnonymousProfileAppearanceCreateDTO
{
    public int JobCategoryId { get; set; }

    public string PreferredPositions { get; set; }

    public IEnumerable<int> WorkplaceTypes { get; set; }

    public IEnumerable<int> JobEngagements { get; set; }

    public IEnumerable<int> SoftSkills { get; set; }

    public IEnumerable<int> ITAreas { get; set; }

    public IEnumerable<int> TechStacks { get; set; }
}
