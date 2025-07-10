using System.Collections.Generic;

namespace JobFinder.Web.Models.Account
{
    public class LoginResult
    {
        public string Message { get; set; }

        public string Token { get; set; }

        public string Username { get; set; }

        public string Id { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
