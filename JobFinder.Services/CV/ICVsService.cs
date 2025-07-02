using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.Cv
{
    public interface ICvsService
    {
        Task<string> CreateAsync(CVCreateInputModel cvModel, string userId);

        Task<IEnumerable<CvListingModel>> All(string userId);

        Task<MyCvDataViewModel> GetOwnCvData(string cvId, string userId);

        Task<CvPreviewDataViewModel> GetUserCvData(string cvId);

        Task<CvPreviewDataViewModel> GetRequestedCvData(int cvRequestId, string currentUserId);

        Task Delete(string id);

        Task<string> GetOwnerId(string id);

        Task ValidateApplicationIsSentForCurrentUserJobAd(string cvId, int jobAdId, string currentUserId);

        Task<byte[]> GeneratePdf(string id, string userId);
    }
}
