﻿namespace JobFinder.Services.CV
{
    using JobFinder.Web.Models.CVModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICVsService
    {
        Task<bool> ExistsAsync(string id);

        Task<string> CreateAsync(CVCreateInputModel cvModel, string userId);

        Task<IEnumerable<T>> AllAsync<T>(string userId);

        Task<T> GetDataAsync<T>(string cvId);

        Task<bool> SetDataAsync(string cvId, byte[] data);

        Task<byte[]> GetCvDataAsync(string cvId);

        Task<bool> DeleteCvAsync(string id, string userId);

        Task<string> GetOwnerId(string cvId);
    }
}
