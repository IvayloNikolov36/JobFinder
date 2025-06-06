﻿using JobFinder.Web.Models.CVModels;

namespace JobFinder.Services.CV
{
    public interface ICVsService
    {
        Task<string> CreateAsync(CVCreateInputModel cvModel, string userId);

        Task<IEnumerable<CvListingModel>> All(string userId);

        Task<MyCvDataViewModel> GetOwnCvData(string cvId, string userId);

        Task<CvPreviewDataViewModel> GetUserCvData(string cvId);

        Task SetData(string id, byte[] data);

        Task<byte[]> GetCvDataAsync(string id);

        Task Delete(string id);

        Task<string> GetOwnerId(string id);

        Task ValidateApplicationIsSentForCurrentUserJobAd(string cvId, int jobAdId, string currentUserId);
    }
}
