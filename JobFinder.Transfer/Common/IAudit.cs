namespace JobFinder.Transfer.Common
{
    public interface IAudit
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
