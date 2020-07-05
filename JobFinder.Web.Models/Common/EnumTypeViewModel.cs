namespace JobFinder.Web.Models.Common
{
    public class EnumTypeViewModel
    {
        public EnumTypeViewModel(int value, string viewValue)
        {
            this.Value = value;
            this.ViewValue = viewValue;
        }

        public int Value { get; set; }

        public string ViewValue { get; set; }
    }
}
