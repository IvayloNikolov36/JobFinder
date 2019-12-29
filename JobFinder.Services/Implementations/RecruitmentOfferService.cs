using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobFinder.Data;
using JobFinder.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Services.Implementations
{
    public class RecruitmentOfferService : IRecruitmentOfferService
    {
        private readonly JobFinderDbContext dbContext;

        public RecruitmentOfferService(JobFinderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RecruitmentOffer> GetAsync(int id)
        {
            return await this.dbContext.FindAsync<RecruitmentOffer>(id);
        }

        public async Task CreateAsync(string publisherId, string position, string description, DateTime expiration)
        {
            var offer = new RecruitmentOffer
            {
                Position = position,
                Desription = description,
                PostedOn = DateTime.UtcNow,
                Expiration = expiration,
                PublisherId = publisherId
            };

            await this.dbContext.AddAsync(offer);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RecruitmentOffer>> AllAsync()
        {
            return await this.dbContext.RecruitmentOffers.ToListAsync();
        }

        public async Task EditAsync(int offerId, string position, string description, int daysActive)
        {
            var offerFromDb = await this.dbContext.FindAsync<RecruitmentOffer>(offerId);

            offerFromDb.Position = position;
            offerFromDb.Desription = description;
            offerFromDb.Expiration = DateTime.UtcNow.AddDays(daysActive);

            await this.dbContext.SaveChangesAsync();
        }
    }
}
