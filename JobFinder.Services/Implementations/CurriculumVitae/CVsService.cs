namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Data;
    using JobFinder.Services.CurriculumVitae;
    using System.Threading.Tasks;
    using JobFinder.Data.Models.CV;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using JobFinder.Services.Mappings;

    public class CVsService : DbService, ICVsService
    {
        public CVsService(JobFinderDbContext dbContext) 
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<T>> AllAsync<T>(string userId)
        {
            return await this.DbContext.CVs.AsNoTracking()
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

            await this.DbContext.AddAsync(cv);
            await this.DbContext.SaveChangesAsync();

            return cv.Id;
        }

        public async Task<byte[]> GetCvDataAsync(string cvId)
        {
            byte[] data = await this.DbContext.CVs.AsNoTracking()
                .Where(cv => cv.Id == cvId)
                .Select(cv => cv.Data)
                .FirstOrDefaultAsync();

            return data;
        }

        public async Task<T> GetDataAsync<T>(string cvId)
        {
            var data = await this.DbContext.CVs.AsNoTracking()
                .Where(cv => cv.Id == cvId)
                .To<T>()
                .SingleOrDefaultAsync();

            return data;
        }

        public async Task<bool> SetDataAsync(string cvId, byte[] data)
        {
            var cvFromDb = await this.DbContext.FindAsync<CurriculumVitae>(cvId);

            if (cvFromDb == null)
            {
                return false;
            }

            cvFromDb.Data = data;
            await this.DbContext.SaveChangesAsync();

            return true;
        }
    }
}
