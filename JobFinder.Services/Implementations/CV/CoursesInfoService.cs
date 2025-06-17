using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.Cv;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.Implementations.Cv
{
    public class CoursesInfoService : ICoursesInfoService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CoursesInfoService(
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
