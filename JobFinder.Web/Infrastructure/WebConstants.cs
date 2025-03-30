namespace JobFinder.Web.Infrastructure
{
    public class WebConstants
    {
        public const string AdminRole = "Admin";
        public const string CompanyRole = "Company";

        public const string AdminUserName = "Administrator";
        public const string AdminEmail = "admin@job-finder.com";
        public const string DefaultAdminPassword = "AdminA123";
        
        // CORS
        public const string CorsPolicyName = "JobFinderCORSPolicy";
        public const string HttpGet = "GET";
        public const string HttpPost = "POST";
        public const string HttpPut = "PUT";
        public const string HttpPatch = "PATCH";
        public const string HttpDelete = "DELETE";

        // CRONS
        public const string DailyCronExpression = "0 0 * * *";
        public const string EverySundayCronExpression = "0 0 * * SUN";
        public const string FirstDayOfTheMonthCronExpression = "0 0 1 * *";

        public const string RegisterSuccess = "Successfully registered!";
        public const string LoginSuccess = "Successfully logged in!";
        public const string InvalidEmailOrPassword = "Email and/or password are invalid!";
        public const string CanNotAddCompanyRole = "Unable to add company role!";

        // Job Ads
        public const string NoJobFound = "Job ad not found!";
        public const string SuccessOnCreation = "Successfully created a job ad!";
        public const string CantEditAd = "Edit does not exist or you are trying to edit other companie job ad!";
        public const string UpdatedAd = "Job ad successfully updated!";

        // Company
        public const string ExistingCompany = "There is already a company with this name or bulstat!"; 
    }
}
