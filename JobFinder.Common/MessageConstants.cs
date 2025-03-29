namespace JobFinder.Common
{
    public class MessageConstants
    {
        public const string SpecifyCurrency = "You have to specify currency type!";
        public const string SpecifyMinSalary = "You specified currency and max salary but forgot to specify min salary!";
        public const string SpecifyMaxSalary = "You specified currency and min salary but forgot to specify max salary!";
        public const string MaxSalaryRestriction = "Max Salary must be equal to or grater than Min Salary!";
        public const string SpecifyMinAndMaxSalary = "You have to specify both min and max salary!";
        public const string IntershipAppropriateEngagements = "When selecting Intership, you have to select one of these Job Engagements: {0}";
        public const string NoSubscriptionCriterias = "No criterias specified for a subscription!";
        public const string ErrorModelPropertyMessage = "The {0} must be at least {2} and at max {1} characters long.";
        public const string InvalidEmailOrPassword = "Email and/or password are invalid!";
        public const string LoginSuccess = "Successfully logged in!";
    }
}
