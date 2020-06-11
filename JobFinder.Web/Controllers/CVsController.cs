using JobFinder.Web.Models.Candidates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace JobFinder.Web.Controllers
{
    public class CVsController : ApiController
    {
        [HttpPost("upload")]
        [Authorize]
        public async Task<ActionResult> UploadCv([FromForm] CurriculumVitaeUploadModel model)
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
