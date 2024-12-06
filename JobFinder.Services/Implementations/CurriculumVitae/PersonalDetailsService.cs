namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class PersonalDetailsService : IPersonalDetailsService
    {
        private readonly IRepository<PersonalInfo> repository;
        private readonly IMapper mapper;

        public PersonalDetailsService(
            IRepository<PersonalInfo> personalDetailsRepository,
            IMapper mapper) 
        {
            this.repository = personalDetailsRepository;
            this.mapper = mapper;
        }

        public async Task<T> GetAsync<T>(string cvId)
        {
            T personalDetails = await this.repository.AllAsNoTracking()
                .Where(pd => pd.CurriculumVitaeId == cvId)
                .To<T>()
                .FirstOrDefaultAsync();

            return personalDetails;
        }

        public async Task<bool> UpdateAsync(PersonalDetailsEditModel personalDetails)
        {
            PersonalInfo personalDetailsFromDb = await this.repository.FindAsync(personalDetails.Id);

            if (personalDetailsFromDb == null)
            {
                return false;
            }

            this.mapper.Map(personalDetails, personalDetailsFromDb);

            this.repository.Update(personalDetailsFromDb);
            await this.repository.SaveChangesAsync();

            return true;
        }
    }
}
