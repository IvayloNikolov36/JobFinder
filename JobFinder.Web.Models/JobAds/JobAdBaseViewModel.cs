using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.JobAds;

public class JobAdBaseViewModel
{
    [Required]
    [StringLength(90, MinimumLength = 6)]
    public string Position { get; set; }

    [Required]
    [MinLength(20)]
    public string Description { get; set; }

    public int LocationId { get; set; }

    public int? MinSalary { get; set; }

    public int? MaxSalary { get; set; }

    public int? CurrencyId { get; set; }

    public int JobCategoryId { get; set; }

    public int JobEngagementId { get; set; }

    public bool Intership { get; set; }

    public int WorkplaceTypeId { get; set; }

    public IEnumerable<int> SoftSkills { get; set; }

    public IEnumerable<int> ITAreas { get; set; }

    public IEnumerable<int> TechStacks { get; set; }
}
