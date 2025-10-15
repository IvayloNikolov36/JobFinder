using JobFinder.Data.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models;

public partial class CloudImageEntity : BaseEntity<int>
{
    [Required]
    public string PublicId { get; set; }

    [Required]
    public string Url { get; set; }

    [Required]
    public string ThumbnailUrl { get; set; }

    public long Size { get; set; }

    [Required]
    public string UserId { get; set; }
    public UserEntity User { get; set; }
}
