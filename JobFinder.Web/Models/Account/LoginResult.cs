namespace JobFinder.Web.Models.Account
{
    public class LoginResult
    {
        public bool Successful { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }
    }
}
