namespace JobFinder.Data.Models.Common
{
    using System;

    public interface IAuditInfo
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
