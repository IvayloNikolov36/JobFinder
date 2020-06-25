namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Data;
    using JobFinder.Services.CurriculumVitae;
    using System.Threading.Tasks;
    using JobFinder.Data.Models.CV;

    public class CVsService : DbService, ICVsService
    {
        public CVsService(JobFinderDbContext dbContext) 
            : base(dbContext)
        {

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
    }
}
