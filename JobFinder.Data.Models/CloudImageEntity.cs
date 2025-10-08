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

    [Required]
    [StringLength(10, MinimumLength = 3)]
    public string Extension { get; set; }

    public long Size { get; set; }

    [Required]
    public string UploaderId { get; set; }
    public UserEntity Uploader { get; set; }

    public CompanyEntity CompanyLogo { get; set; }
}
