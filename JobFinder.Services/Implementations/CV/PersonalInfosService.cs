using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CV;
using JobFinder.Transfer.DTOs.CV;
using JobFinder.Web.Models.CVModels;

namespace JobFinder.Services.Implementations.CV
{
    public class PersonalInfosService : IPersonalInfosService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PersonalInfosService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Update(PersonalInfoEditModel personalInfo)
        {
            PersonalInfoEditDTO personalInfoDto = this.mapper.Map<PersonalInfoEditDTO>(personalInfo);

            await this.unitOfWork.PersonalInfoRepository.Update(personalInfoDto);

            await this.unitOfWork.SaveChanges();
        }
    }
}
