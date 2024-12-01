namespace JobFinder.Web.Models.Common
{
    public class BasicValueViewModel
    {
        public BasicValueViewModel(int value, string viewValue)
        {
            this.Value = value;
            this.ViewValue = viewValue;
        }

        public int Value { get; set; }

        public string ViewValue { get; set; }
    }
}
