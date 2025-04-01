namespace JobFinder.Services.Implementations.CV
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.DataAccess.Generic;
    using JobFinder.Services.CV;
    using JobFinder.Web.Models.CVModels;
    using System.Threading.Tasks;

    public class PersonalInfosService : IPersonalInfosService
    {
        private readonly IRepository<PersonalInfoEntity> repository;
        private readonly IMapper mapper;

        public PersonalInfosService(
            IRepository<PersonalInfoEntity> personalDetailsRepository,
            IMapper mapper) 
        {
            this.repository = personalDetailsRepository;
            this.mapper = mapper;
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
