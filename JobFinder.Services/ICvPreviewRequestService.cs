namespace JobFinder.Services;

public interface ICvPreviewRequestService
{
    Task AllowCvPreview(int id, string currentUserId);
}
