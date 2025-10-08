using JobFinder.Transfer.Common;

namespace JobFinder.Transfer.DTOs;

public class CloudImageDTO : IUniquelyIdentified<int>
{
    public int Id { get; set; }

    public required string PublicId { get; set; }

    public required string Url { get; set; }

    public required string ThumbnailUrl { get; set; }

    public required string Extension { get; set; }

    public required long Size { get; set; }

    public required string UploaderId { get; set; }

    public Guid UniqueIdentificator { get; set; }
}
