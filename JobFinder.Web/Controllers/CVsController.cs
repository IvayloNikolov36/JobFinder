namespace JobFinder.Web.Controllers
{
    using JobFinder.Web.Models.Candidates;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.IO;

    public class CVsController : ApiController
    {
        [HttpPost("upload")]
        [Authorize]
        public ActionResult UploadCv([FromForm] CVUploadModel model)
        {
            var file = model.CurriculumVitae;
            var fileName = model.CurriculumVitae.FileName;

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    byte[] fileBytes = ms.ToArray();
                }
            }

            return this.Ok(new { FileName = fileName });
        }
    }
}
