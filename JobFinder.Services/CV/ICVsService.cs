using JobFinder.Web.Models.CVModels;

namespace JobFinder.Services.CV
{
    public interface ICVsService
    {
        Task<bool> ExistsAsync(string id);

        Task<string> CreateAsync(CVCreateInputModel cvModel, string userId);

        Task<IEnumerable<T>> AllAsync<T>(string userId);

        Task<MyCvDataViewModel> GetOwnCvData(string cvId, string userId);

        Task<T> GetOwnCvDataAsync<T>(string cvId, string currentUserId);

        Task<CvPreviewDataViewModel> GetUserCvData(string cvId, int jobAdId, string currentUserId);

        Task<bool> SetDataAsync(string id, byte[] data);

        Task<byte[]> GetCvDataAsync(string id);

        Task DeleteCvAsync(string id);

        Task<string> GetOwnerId(string id);

        Task ValidateCvIsSentForCurrentUsersJobAd(string cvId, int jobAdId, string currentUserId);
    }
}
