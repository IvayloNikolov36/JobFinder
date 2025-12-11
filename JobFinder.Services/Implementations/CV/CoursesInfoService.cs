using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.Cv;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.Extensions.Caching.Distributed;
using static JobFinder.Services.Constants.CacheConstants;

namespace JobFinder.Services.Implementations.Cv
{
    public class CoursesInfoService : ICoursesInfoService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;
        public CoursesInfoService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper,
            IDistributedCache distributedCache)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
        }

        public async Task<IEnumerable<CourseInfoViewModel>> All(string cvId)
        {
            IEnumerable<CourseCertificateDTO> coursesData = await this.unitOfWork
                .CoursesCertificateInfoRepository
                .All(cvId);

            return this.mapper.Map<IEnumerable<CourseInfoViewModel>>(coursesData);
        }

        public async Task<UpdateResult<int>> Update(
            string cvId,
            IEnumerable<CourseSertificateEditModel> coursesInfoModels)
        {
            IEnumerable<CourseCertificateSimpleDTO> courcesInfo = this.mapper
                .Map<IEnumerable<CourseCertificateSimpleDTO>>(coursesInfoModels);

            IEnumerable<CourseCertificateSimpleDTO> newItems = await this.unitOfWork
                .CoursesCertificateInfoRepository
                .Update(cvId, courcesInfo);

            await this.unitOfWork.SaveChanges<CourseCertificateSimpleDTO, int>(newItems);

            string cvCacheKey = string.Format(CvCacheKey, cvId);

            await this.distributedCache.RemoveAsync(cvCacheKey);

            return new UpdateResult<int>(newItems);
        }

        public async Task Delete(int id)
        {
            await this.unitOfWork.CoursesCertificateInfoRepository.Delete(id);

            await this.unitOfWork.SaveChanges();
        }
    }
}
