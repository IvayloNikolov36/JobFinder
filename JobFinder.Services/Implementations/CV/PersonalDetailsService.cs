namespace JobFinder.Services.Implementations.CV
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CV;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class PersonalDetailsService : IPersonalDetailsService
    {
        private readonly IRepository<PersonalInfoEntity> repository;
        private readonly IMapper mapper;

        public PersonalDetailsService(
            IRepository<PersonalInfoEntity> personalDetailsRepository,
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
            PersonalInfoEntity personalDetailsFromDb = await this.repository.FindAsync(personalDetails.Id);

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
