using JobFinder.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobFinder.Services
{
    public interface IJobAdsService
    {
        Task<RecruitmentOffer> GetAsync(int id); 

        Task CreateAsync(string publisherId, string position, string description, DateTime expiration);

        Task<IEnumerable<RecruitmentOffer>> AllAsync();

        Task EditAsync(int offerId, string position, string description, int daysActive);
    }
}
