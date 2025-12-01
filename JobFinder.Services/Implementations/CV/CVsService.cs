using AutoMapper;
using JobFinder.Business.CourseCertificatesInfo;
using JobFinder.Common.Exceptions;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.Cv;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using static JobFinder.Services.Constants.CacheConstants;

namespace JobFinder.Services.Implementations.Cv
{
    public class CvsService : ICvsService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;
        private readonly ICloudImageManagementService cloudImageManagementService;
        private readonly IPdfGenerator pdfGenerator;
        private readonly ICourseCertificateInfoRules courceCertificateInfoRules;

        public CvsService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper,
            IDistributedCache distributedCache,
            ICloudImageManagementService cloudImageManagementService,
            IPdfGenerator pdfGenerator,
            ICourseCertificateInfoRules courceCertificateInfoRules)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.cloudImageManagementService = cloudImageManagementService;
            this.distributedCache = distributedCache;
            this.pdfGenerator = pdfGenerator;
            this.courceCertificateInfoRules = courceCertificateInfoRules;
        }

        public async Task<IEnumerable<CvListingModel>> All(string userId)
        {
            string cacheKey = string.Format(CVsCacheKey, userId);

            byte[] cachedData = await this.distributedCache.GetAsync(cacheKey);
            if (cachedData == null)
            {
                IEnumerable<CVListingDTO> cvDtos = await this.unitOfWork
                    .CvRepository
                    .All(userId);

                var data = this.mapper.Map<IEnumerable<CvListingModel>>(cvDtos);

                string serializedData = JsonSerializer.Serialize(data);

                await this.distributedCache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(serializedData));

                return data;
            }

            return JsonSerializer
                .Deserialize<IEnumerable<CvListingModel>>(Encoding.UTF8.GetString(cachedData));
        }

        public async Task<IdentityViewModel<string>> CreateAsync(
            CVCreateInputModel cvModel,
            string userId,
            bool invalidateCache)
        {
            var coursesDtos = this.mapper
                .Map<IEnumerable<CourseCertificateInputDTO>>(cvModel.CourseCertificates);

            this.courceCertificateInfoRules.ValidateData(coursesDtos);

            CVCreateDTO cvDataDto = this.mapper.Map<CVCreateDTO>(cvModel);

            await this.unitOfWork.CvRepository.Create(userId, cvDataDto);

            await this.unitOfWork.SaveChanges<CVCreateDTO, string>(cvDataDto);

            if (invalidateCache)
            {
                await this.distributedCache
                    .RemoveAsync(string.Format(CVsCacheKey, userId));
            }

            return new IdentityViewModel<string>(cvDataDto.Id);
        }

        public async Task<MyCvDataViewModel> GetOwnCvData(string cvId, string userId)
        {
            string cacheKey = string.Format(CvCacheKey, cvId);

            byte[] cachedCv = await this.distributedCache
                .GetAsync(cacheKey);

            if (cachedCv == null)
            {
                MyCvDataDTO cvDataDto = await this.unitOfWork
                    .CvRepository
                    .GetCvData<MyCvDataDTO>(cvId);

                var cvData = this.mapper.Map<MyCvDataViewModel>(cvDataDto);

                bool hasAnyAnonymousProfileActivated = await this.unitOfWork
                    .AnonymousProfileRepository
                    .HasAnonymousProfile(userId);

                cvData.CanActivateAnonymousProfile = !hasAnyAnonymousProfileActivated;

                cvData.PictureUrl = await this.cloudImageManagementService
                    .GetUrl(cvDataDto.PictureId);

                cvData.PictureThumbnailUrl = await this.cloudImageManagementService
                    .GetThumbnailUrl(cvDataDto.PictureId);

                string serializedCvData = JsonSerializer.Serialize(cvData);

                await this.distributedCache.SetAsync(
                    cacheKey,
                    Encoding.UTF8.GetBytes(serializedCvData)
                );

                return cvData;
            }

            return JsonSerializer.Deserialize<MyCvDataViewModel>(cachedCv);
        }

        public async Task<CvPreviewDataViewModel> GetUserCvData(string cvId)
        {
            CvPreviewDataDTO cvData = await this.unitOfWork
                .CvRepository
                .GetCvData<CvPreviewDataDTO>(cvId);

            return this.mapper.Map<CvPreviewDataViewModel>(cvData);
        }

        public async Task<CvPreviewDataViewModel> GetRequestedCvData(int cvRequestId, string currentUserId)
        {
            string requesterId = await this.unitOfWork
                .CvPreviewRequestRepository
                .GetRequesterId(cvRequestId);

            if (requesterId != currentUserId)
            {
                throw new ActionableException("You are not allowed to access CV from foreign CV request!");
            }

            CvPreviewDataDTO cvData = await this.unitOfWork
                .CvRepository
                .GetRequestedCvData(cvRequestId);

            var model = this.mapper.Map<CvPreviewDataViewModel>(cvData);

            if (cvData.PictureId.HasValue)
            {
                model.PictureUrl = await this.cloudImageManagementService.GetThumbnailUrl(cvData.PictureId.Value);
            }

            return model;
        }

        public async Task Delete(string cvId, string userId)
        {
            this.unitOfWork.PersonalInfoRepository.Delete(cvId);
            this.unitOfWork.EducationInfoRepository.Delete(cvId);
            this.unitOfWork.WorkExperienceRepository.Delete(cvId);
            this.unitOfWork.LanguageInfoRepository.Delete(cvId);
            this.unitOfWork.CoursesCertificateInfoRepository.Delete(cvId);
            this.unitOfWork.SkillsInfoDrivingCategoryRepository.Delete(cvId);
            await this.unitOfWork.SkillsInfoRepository.Delete(cvId);

            this.unitOfWork.JobAdApplicationsRepository
                .DeleteAll(cvId);

            this.unitOfWork.CvPreviewRequestRepository
                .DeleteAll(cvId);

            await this.unitOfWork.AnonymousProfileRepository
                .DeleteAnonymousProfile(cvId);

            await this.unitOfWork.CvRepository.Delete(cvId);

            await this.unitOfWork.SaveChanges();

            string cvsCacheKey = string.Format(CVsCacheKey, userId);
            await this.distributedCache.RemoveAsync(cvsCacheKey);
        }

        public async Task<string> GetOwnerId(string cvId)
        {
            return await this.unitOfWork.CvRepository.GetUserId(cvId);
        }

        public async Task ValidateApplicationIsSentForCurrentUserJobAd(string cvId, int jobAdId, string currentUserId)
        {
            bool isCvSentForCurrentUsersJobAd = await this.unitOfWork
                .JobAdApplicationsRepository
                .IsApplicationSent(cvId, jobAdId, currentUserId);

            if (!isCvSentForCurrentUsersJobAd)
            {
                throw new ActionableException("You are not allowed to access data for job applications sent to other companies ads!");
            }

            return;
        }

        public async Task<byte[]> GeneratePdf(string id, string userId)
        {
            MyCvDataViewModel data = await this.GetOwnCvData(id, userId);

            StringBuilder sb = new();

            sb.AppendFormat(@$"
                <html>
                    <head></head>
                    <body>
                        <div class='header'>
                            <h1>Curriculum Vitae</h1>
                        </div>
            ");

            this.AppendPersonalInfo(sb, data.PersonalInfo);
            this.AppendWorkExperienceInfo(sb, data.WorkExperiences);
            this.AppendEducationInfo(sb, data.Educations);

            sb.Append(@"
                <table align='center'>
                    <tr>
                        <th>Personal skills and competences</th>
                        <th></th>
                    </tr>
            ");

            this.AppendLanguagesInfo(sb, data.LanguagesInfo);
            this.AppendSkillsInfo(sb, data.Skills);
            this.AppendCoursesInfo(sb, data.CourseCertificates);

            sb.Append("</table>");

            sb.Append(@"</body>
                        </html>");

            byte[] cvData = pdfGenerator.Generate(sb.ToString());

            return cvData;
        }

        private void AppendPersonalInfo(StringBuilder sb, PersonalInfoViewModel personalInfo)
        {
            sb.AppendLine(@$"
                <table align='center'>
                    <tr>
                        <th>Personal Details</th>
                        <th></th>
                    </tr>
            ");

            sb.AppendFormat(@$"
                <tr>
                    <td>Name</td>
                    <td>
                        {this.GetPersonalInfoFullName(personalInfo)}
                    </td>
                </tr>
            ");

            sb.AppendFormat(@$"
                <tr>
                    <td>Phone</td>
                    <td>{personalInfo.Phone}</td>
                </tr>
            ");

            sb.AppendFormat(@$"
                <tr>
                    <td>Email</td>
                    <td>{personalInfo.Email}</td>
                </tr>
            ");

            sb.AppendFormat(@$"
                <tr>
                    <td>Citizenship</td>
                    <td>{personalInfo.Citizenship}</td>
                </tr>
            ");

            sb.AppendFormat(@$"
                <tr>
                    <td>I live in</td>
                    <td>{personalInfo.City}, {personalInfo.Country}</td>
                </tr>
            ");

            sb.AppendFormat(@$"
                <tr>
                    <td>Gender</td>
                    <td>{personalInfo.Gender}</td>
                </tr>
            ");

            sb.Append("</table>");
        }

        private string GetPersonalInfoFullName(PersonalInfoViewModel personalInfo)
        {
            return $"{personalInfo.FirstName} {personalInfo.MiddleName} {personalInfo.LastName}";
        }

        private void AppendCoursesInfo(StringBuilder sb, IEnumerable<CourseInfoViewModel> courseCertificates)
        {
            foreach (CourseInfoViewModel cs in courseCertificates)
            {
                sb.AppendFormat(@$"
                    <tr>
                        <td>{cs.CourseName}</td>
                        <td>{cs.CertificateUrl}</td>
                    </tr>
                ");
            }
        }

        private void AppendSkillsInfo(StringBuilder sb, SkillsViewModel skills)
        {
            sb.AppendFormat(@$"
                <tr>
                    <td>Computer skills and competences</td>
                    <td>{skills.ComputerSkills}</td>
                </tr>
            ");

            sb.AppendFormat(@$"
                <tr>
                    <td>Other skills and competences</td>
                    <td>{skills.OtherSkills}</td>
                </tr>
            ");

            sb.AppendFormat(@$"
                <tr>
                    <td>Driving license</td>
                    <td></td>
                </tr>
            ");
        }

        private void AppendLanguagesInfo(StringBuilder sb, IEnumerable<LanguageInfoViewModel> languagesInfo)
        {
            foreach (LanguageInfoViewModel l in languagesInfo)
            {
                sb.AppendFormat(@$"
                    <tr>
                        <td>Language</td>
                        <td>{l.LanguageType}</td>
                    </tr>
                ");

                sb.AppendFormat(@$"
                    <tr>
                        <td>Comprehension</td>
                        <td>{l.ComprehensionLevel.Name}</td>
                    </tr>
                ");

                sb.AppendFormat(@$"
                    <tr>
                        <td>Speaking</td>
                        <td>{l.SpeakingLevel.Name}</td>
                    </tr>
                ");

                sb.AppendFormat(@$"
                    <tr>
                        <td>Writing</td>
                        <td>{l.WritingLevel.Name}</td>
                    </tr>
                ");
            }
        }

        private void AppendEducationInfo(StringBuilder sb, IEnumerable<EducationViewModel> educationInfo)
        {
            sb.Append(@"
                <table align='center'>
                    <tr>
                        <th>Education</th>
                        <th></th>
                    </tr>
            ");

            foreach (EducationViewModel e in educationInfo)
            {
                sb.AppendFormat(@$"
                    <tr>
                        <td>Dates</td>
                        <td>{e.FromDate} - {e.ToDate}</td>
                    </tr>
                ");

                sb.AppendFormat(@$"
                    <tr>
                        <td>Principal subjects/occupational skills covered</td>
                        <td>{e.MainSubjects}</td>
                    </tr>
                ");

                sb.AppendFormat(@$"
                    <tr>
                        <td>Name and type of organisation providing education and training</td>
                        <td>{e.Organization}</td>
                    </tr>
                ");

                sb.AppendFormat(@$"
                    <tr>
                        <td>Location</td>
                        <td>{e.Location}</td>
                    </tr>
                ");

                sb.AppendFormat(@$"
                    <tr>
                        <td>Level</td>
                        <td>{e.Major}</td>
                    </tr>
                ");
            }

            sb.Append("</table>");
        }

        private void AppendWorkExperienceInfo(StringBuilder sb, IEnumerable<WorkExperienceViewModel> workExperienceinfo)
        {
            sb.Append(@"<table align='center'>
                            <tr>
                                <th>Work Experience</th>
                                <th></th>
                            </tr>");

            foreach (WorkExperienceViewModel we in workExperienceinfo)
            {
                sb.AppendFormat(@$"
                    <tr>
                        <td>Dates</td>
                        <td>{we.FromDate} - {we.ToDate}</td>
                    </tr>
                ");

                sb.AppendFormat(@$"
                    <tr>
                        <td>Occupation or position held</td>
                        <td>{we.JobTitle}</td>
                    </tr>
                ");

                sb.AppendFormat(@$"
                    <tr>
                        <td>Name of employer</td>
                        <td>{we.Organization}</td>
                    </tr>
                ");

                sb.AppendFormat(@$"
                    <tr>
                        <td>Location</td>
                        <td>{we.Location}</td>
                    </tr>
                ");

                sb.AppendFormat(@$"
                    <tr>
                        <td>Type of business</td>
                        <td>{we.BusinessSector}</td>
                    </tr>
                ");
            }

            sb.Append("</table>");
        }
    }
}
