namespace JobFinder.Web.Infrastructure
{
    public class WebConstants
    {
        public const string AdminRole = "Admin";
        public const string AdminUserName = "Administrator";
        public const string AdminEmail = "admin@job-finder.com";
        public const string DefaultAdminPassword = "AdminA123";

        public const string CompanyRole = "Company";

        //Job Ads
        public const string NoJobFound = "Job ad not found!";
        public const string SuccessOnCreation = "Successfully created a job ad!";
        public const string CantEditAd = "Edit does not exist or you are trying to edit other companie job ad!";
        public const string UpdatedAd = "Job ad successfully updated!";


        //CORS
        public const string CorsPolicyName = "JobFinderCORSPolicy";
    }
}
