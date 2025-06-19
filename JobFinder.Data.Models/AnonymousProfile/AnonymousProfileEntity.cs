using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Cv;
using System;

namespace JobFinder.Data.Models.AnonymousProfile;

public partial class AnonymousProfileEntity : BaseEntity<string>
{
    public AnonymousProfileEntity()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public string UserId { get; set; }
    public UserEntity User { get; set; }

    public string CvId { get; set; }
    public CurriculumVitaeEntity Cv { get; set; }

    public AnonymousProfileAppearanceEntity Appearance { get; set; }
}
