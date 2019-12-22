using System.Collections.Generic;

namespace JobFinder.Web.Models.Account
{
    public class RegisterResult
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
