namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    [Authorize]
    public class CVsController : ApiController
    {
        private readonly ICVsService cvsService;

        public CVsController(ICVsService cvsService)
        {
            this.cvsService = cvsService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] CVCreateInputModel model)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string cvId = await this.cvsService.CreateAsync(userId, model.Name, model.PictureUrl);

            return this.Ok(new { cvId });
        }

        [HttpGet("{id:guid}")]
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
        public async Task<ActionResult<IEnumerable<CvListingModel>>> All()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var cvs = await this.cvsService.AllAsync<CvListingModel>(userId);

            return this.Ok(cvs);
        }

        //[HttpGet]
        //public async Task<IActionResult<CvListingModel>> GetCv(Guid id)
        //{

        //}

        [HttpGet("generate/{id}")]
        public async Task<ActionResult> GeneratePdf(string id, [FromServices] IPdfGenerator pdfGenerator)
        {
            CvDataViewModel data = await this.cvsService.GetDataAsync<CvDataViewModel>(cvId: id);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@$"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Curriculum Vitae</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Personal Details</th>
                                        <th></th>
                                    </tr>");

            sb.AppendFormat(@$"<tr>
                                    <td>Name</td>
                                    <td>{data.PersonalDetails.FirstName + ' ' + data.PersonalDetails.MiddleName + ' ' + data.PersonalDetails.LastName }</td>");
            sb.AppendFormat(@$"<tr>
                                    <td>Phone</td>
                                    <td>{data.PersonalDetails.Phone}</td>");
            sb.AppendFormat(@$"<tr>
                                    <td>Email</td>
                                    <td>{data.PersonalDetails.Email}</td>");
            sb.AppendFormat(@$"<tr>
                                    <td>Citizenship</td>
                                    <td>{data.PersonalDetails.CitizenShip}</td>");
            sb.AppendFormat(@$"<tr>
                                    <td>I live in</td>
                                    <td>{data.PersonalDetails.City}, {data.PersonalDetails.Country}</td>");
            sb.AppendFormat(@$"<tr>
                                    <td>Gender</td>
                                    <td>{data.PersonalDetails.Gender}</td>");
            sb.Append("</table>");

            //Work experience
            sb.Append(@"<table align='center'>
                                    <tr>
                                        <th>Work Experience</th>
                                        <th></th>
                                    </tr>");
            foreach (var we in data.WorkExperiences)
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

            //Education
            sb.Append(@"<table align='center'>
                                    <tr>
                                        <th>Education</th>
                                        <th></th>
                                    </tr>");
            foreach (var e in data.Educations)
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

            //Personal skills and competences
            sb.Append(@"<table align='center'>
                                    <tr>
                                        <th>Personal skills and competences</th>
                                        <th></th>
                                    </tr>");
            //languages
            foreach (var l in data.LanguagesInfo)
            {
                sb.AppendFormat(@$"<tr>
                                    <td>Language</td>
                                    <td>{l.LanguageType}</td>");

                sb.AppendFormat(@$"<tr>
                                    <td>Comprehension</td>
                                    <td>{l.Comprehension}</td>");
                sb.AppendFormat(@$"<tr>
                                    <td>Speaking</td>
                                    <td>{l.Speaking}</td>");
                sb.AppendFormat(@$"<tr>
                                    <td>Writing</td>
                                    <td>{l.Writing}</td>");
            }
            //skills
            
            sb.AppendFormat(@$"<tr>
                                    <td>Computer skills and competences</td>
                                    <td>{data.Skills.ComputerSkills}</td>");
            sb.AppendFormat(@$"<tr>
                                    <td>Other skills and competences</td>
                                    <td>{data.Skills.Skills}</td>");

            //driving license
            sb.AppendFormat(@$"<tr>
                                    <td>Driving license</td>
                                    <td></td>");
            //aditional courses
            foreach (var cs in data.CourseCertificates)
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

            return this.Ok(new { Message = "Successfuly created cv!"});
        }

    }
}
