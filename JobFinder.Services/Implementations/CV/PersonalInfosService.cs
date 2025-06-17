using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CV;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.Implementations.Cv
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
