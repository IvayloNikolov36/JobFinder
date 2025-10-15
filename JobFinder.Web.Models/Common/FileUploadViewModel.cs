using Microsoft.AspNetCore.Http;

namespace JobFinder.Web.Models.Common;

public class FileUploadViewModel
{
    public IFormFile File { get; set; }
}
