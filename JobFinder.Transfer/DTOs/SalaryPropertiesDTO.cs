namespace JobFinder.Transfer.DTOs
{
    public class SalaryPropertiesDTO
    {
        public int? MinSalary { get; set; }

        public int? MaxSalary { get; set; }

        public bool HasCurrencyType { get; set; }
    }
}
