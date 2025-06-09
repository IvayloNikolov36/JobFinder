using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System;

namespace JobFinder.Data.Models.AnonymousProfile;

public class AnonymousProfileEntity : BaseEntity<string>
{
    public AnonymousProfileEntity()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public string UserId { get; set; }
    public UserEntity User { get; set; }

    public string CurriculumVitaeId { get; set; }
    public CurriculumVitaeEntity CurriculumVitae { get; set; }

    public AnonymousProfileAppearanceEntity Appearance { get; set; }
}
