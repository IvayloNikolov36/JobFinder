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

        Task<T> GetDataAsync<T>(string id);

        Task<bool> SetDataAsync(string id, byte[] data);

        Task<byte[]> GetCvDataAsync(string id);

        Task DeleteCvAsync(string id);

        Task<string> GetOwnerId(string id);
    }
}
