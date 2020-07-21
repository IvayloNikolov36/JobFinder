namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using System.Threading.Tasks;
    using JobFinder.Data.Models.CV;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using JobFinder.Services.Mappings;
    using JobFinder.Data.Repositories.Contracts;

    public class CVsService : ICVsService
    {
        private readonly IRepository<CurriculumVitae> repository;

        public CVsService(IRepository<CurriculumVitae> repository) 
        {
            this.repository = repository;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var entity = await this.repository.FindAsync(id);

            return entity != null;
        }

        public async Task<IEnumerable<T>> AllAsync<T>(string userId)
        {
            return await this.repository.AllAsNoTracking()
                .Where(c => c.UserId == userId)
                .To<T>()
                .ToListAsync();
        }

        public async Task<string> CreateAsync(string userId, string name, string pictureUrl)
        {
            var cv = new CurriculumVitae
            {
                UserId = userId,
                Name = name,
                PictureUrl = pictureUrl
            };

            await this.repository.AddAsync(cv);
            await this.repository.SaveChangesAsync();

            return cv.Id;
        }

        public async Task<byte[]> GetCvDataAsync(string cvId)
        {
            byte[] data = await this.repository.AllAsNoTracking()
                .Where(cv => cv.Id == cvId)
                .Select(cv => cv.Data)
                .FirstOrDefaultAsync();

            return data;
        }

        public async Task<T> GetDataAsync<T>(string cvId)
        {
            var data = await this.repository.AllAsNoTracking()
                .Where(cv => cv.Id == cvId)
                .To<T>()
                .SingleOrDefaultAsync();

            return data;
        }

        public async Task<bool> SetDataAsync(string cvId, byte[] data)
        {
            var cvFromDb = await this.repository.FindAsync(cvId);
            if (cvFromDb == null)
            {
                return false;
            }

            cvFromDb.Data = data;
            await this.repository.SaveChangesAsync();

            return true;
        }
    }
}
