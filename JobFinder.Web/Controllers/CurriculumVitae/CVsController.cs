using JobFinder.Services;
using JobFinder.Services.CV;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CVModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers.CurriculumVitae
{
    [Authorize]
    public class CVsController : ApiController
    {
        private readonly ICVsService cvsService;

        public CVsController(ICVsService cvsService)
        {
            this.cvsService = cvsService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<BasicViewModel>> Create([FromBody] CVCreateInputModel cvModel)
        {
            string userId = this.User.GetCurrentUserId();

            string id = await this.cvsService.CreateAsync(cvModel, userId);

            var resultObject = new { id };

            return this.CreatedAtRoute("GetCvData", resultObject, resultObject);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<CvListingModel>>> GetAllMine()
        {
            string userId = this.User.GetCurrentUserId();

            IEnumerable<CvListingModel> myCvs = await this.cvsService.AllAsync<CvListingModel>(userId);

            return this.Ok(myCvs);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCvData")]
        public async Task<ActionResult<CvDataViewModel>> GetCvData([FromRoute] string id)
        {
            string currentUserId = this.User.GetCurrentUserId();

            CvDataViewModel cv = await this.cvsService.GetOwnCvDataAsync<CvDataViewModel>(id, currentUserId);

            return this.Ok(cv);
        }

        [HttpGet]
        [Route("preview/{cvId}/{jobAdId}")]
        [Authorize(Roles = CompanyRole)]
        public async Task<ActionResult<CvDataViewModel>> GetCvPreview([FromRoute] string cvId, [FromRoute] int jobAdId)
        {
            string currentUserId = this.User.GetCurrentUserId();

            CvPreviewDataViewModel cv = await this.cvsService.GetUserCvData(cvId, jobAdId, currentUserId);

            return this.Ok(cv);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> DeleteCv([FromRoute] string id)
        {
            await this.cvsService.DeleteCvAsync(id);

            return this.NoContent();
        }

        [HttpGet]
        [Route("get-pdf/{id:guid}")]
        public async Task<ActionResult> GetCvPdf(Guid id)
        {
            byte[] cvData = await this.cvsService.GetCvDataAsync(id.ToString());
            if (cvData == null)
            {
                return this.BadRequest();
            }

            return this.File(cvData, "application/pdf");
        }


        [HttpGet]
        [Route("generate-pdf/{id}")]
        public async Task<ActionResult> GenerateCVPdf(string id, [FromServices] IPdfGenerator pdfGenerator)
        {
            CvDataPdfViewModel data = await this.cvsService.GetOwnCvDataAsync<CvDataPdfViewModel>(
                id,
                this.User.GetCurrentUserId());

            StringBuilder sb = new();

            sb.AppendFormat(
                @$"<html>
                    <head></head>
                    <body>
                        <div class='header'><h1>Curriculum Vitae</h1></div>
                            <table align='center'>
                                <tr>
                                    <th>Personal Details</th>
                                    <th></th>
                                </tr>");

            sb.AppendFormat(@$"<tr>
                                    <td>Name</td>
                                    <td>
    {data.PersonalDetails.FirstName + ' ' + data.PersonalDetails.MiddleName + ' ' + data.PersonalDetails.LastName }</td>");
            
            sb.AppendFormat(@$"<tr>
                                    <td>Phone</td>
                                    <td>{data.PersonalDetails.Phone}</td>");

            sb.AppendFormat(@$"<tr>
                                    <td>Email</td>
                                    <td>{data.PersonalDetails.Email}</td>");

            sb.AppendFormat(@$"<tr>
                                    <td>Citizenship</td>
                                    <td>{data.PersonalDetails.Citizenship}</td>");

            sb.AppendFormat(@$"<tr>
                                    <td>I live in</td>
                                    <td>{data.PersonalDetails.City}, {data.PersonalDetails.Country}</td>");

            sb.AppendFormat(@$"<tr>
                                    <td>Gender</td>
                                    <td>{data.PersonalDetails.Gender}</td>");

            sb.Append("</table>");

            // Work experience

            sb.Append(@"<table align='center'>
                            <tr>
                                <th>Work Experience</th>
                                <th></th>
                            </tr>");

            foreach (WorkExperienceViewModel we in data.WorkExperiences)
            {
                sb.AppendFormat(@$"<tr>
                                    <td>Dates</td>
                                    <td>{we.FromDate} - {we.ToDate}</td>");

                sb.AppendFormat(@$"<tr>
                                    <td>Occupation or position held</td>
                                    <td>{we.JobTitle}</td>");

                sb.AppendFormat(@$"<tr>
                                    <td>Name of employer</td>
                                    <td>{we.Organization}</td>");

                sb.AppendFormat(@$"<tr>
                                    <td>Location</td>
                                    <td>{we.Location}</td>");

                sb.AppendFormat(@$"<tr>
                                    <td>Type of business</td>
                                    <td>{we.BusinessSector}</td>");
            }

            sb.Append("</table>");

            // Education

            sb.Append(@"<table align='center'>
                            <tr>
                                <th>Education</th>
                                <th></th>
                            </tr>");

            foreach (EducationViewModel e in data.Educations)
            {
                sb.AppendFormat(@$"<tr>
                                    <td>Dates</td>
                                    <td>{e.FromDate} - {e.ToDate}</td>");

                sb.AppendFormat(@$"<tr>
                                    <td>Principal subjects/occupational skills covered</td>
                                    <td>{e.MainSubjects}</td>");

                sb.AppendFormat(@$"<tr>
                                    <td>Name and type of organisation providing education and training</td>
                                    <td>{e.Organization}</td>");

                sb.AppendFormat(@$"<tr>
                                    <td>Location</td>
                                    <td>{e.Location}</td>");

                sb.AppendFormat(@$"<tr>
                                    <td>Level</td>
                                    <td>{e.Major}</td>");
            }

            sb.Append("</table>");

            // Personal skills and Competences

            sb.Append(@"<table align='center'>
                            <tr>
                                <th>Personal skills and competences</th>
                                <th></th>
                            </tr>");

            // Languages

            foreach (LanguageInfoViewModel l in data.LanguagesInfo)
            {
                sb.AppendFormat(@$"<tr>
                                    <td>Language</td>
                                    <td>{l.LanguageType}</td>");

                sb.AppendFormat(@$"<tr>
                                    <td>Comprehension</td>
                                    <td>{l.ComprehensionLevel.Name}</td>");

                sb.AppendFormat(@$"<tr>
                                    <td>Speaking</td>
                                    <td>{l.SpeakingLevel.Name}</td>");

                sb.AppendFormat(@$"<tr>
                                    <td>Writing</td>
                                    <td>{l.WritingLevel.Name}</td>");
            }

            // Skills
            
            sb.AppendFormat(@$"<tr>
                                <td>Computer skills and competences</td>
                                <td>{data.Skills.ComputerSkills}</td>");

            sb.AppendFormat(@$"<tr>
                                <td>Other skills and competences</td>
                                <td>{data.Skills.OtherSkills}</td>");

            // Driving license

            sb.AppendFormat(@$"<tr>
                                <td>Driving license</td>
                                <td></td>");

            // Additional courses

            foreach (CourseInfoViewModel cs in data.CourseCertificates)
            {
                sb.AppendFormat(@$"<tr>
                                    <td>{cs.CourseName}</td>
                                    <td>{cs.CertificateUrl}</td>");
            }

            sb.Append("</table>");

            sb.Append(@"</body>
                        </html>");

            byte[] cvData = pdfGenerator.Generate(sb.ToString());

            bool isDataSet = await this.cvsService.SetDataAsync(id, cvData);
            if (!isDataSet)
            {
                return this.BadRequest();
            }

            return this.Ok(new { Message = "Successfuly created cv pdf!"});
        }
    }
}
