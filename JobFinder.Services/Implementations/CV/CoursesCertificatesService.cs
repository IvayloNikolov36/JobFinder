using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CV;
using JobFinder.Transfer.DTOs.CV;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CVModels;

namespace JobFinder.Services.Implementations.CV
{
    public class CoursesCertificatesService : ICoursesCertificatesService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CoursesCertificatesService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CourseInfoViewModel>> All(string cvId)
        {
            IEnumerable<CourseCertificateDTO> coursesData = await this.unitOfWork
                .CoursesCertificateInfoRepository
                .All(cvId);

            return this.mapper.Map<IEnumerable<CourseInfoViewModel>>(coursesData);
        }

        public async Task<UpdateResult> Update(string cvId, IEnumerable<CourseSertificateEditModel> coursesInfoModels)
        {
            IEnumerable<CourseCertificateSimpleDTO> courcesInfo = this.mapper
                .Map<IEnumerable<CourseCertificateSimpleDTO>>(coursesInfoModels);

            IEnumerable<CourseCertificateSimpleDTO> newItems = await this.unitOfWork
                .CoursesCertificateInfoRepository
                .Update(cvId, courcesInfo);

            await this.unitOfWork.SaveChanges();

            return new UpdateResult(newItems);
        }

        public async Task Delete(int id)
        {
            await this.unitOfWork.CoursesCertificateInfoRepository.Delete(id);

            await this.unitOfWork.SaveChanges();
        }
    }
}
