namespace JobFinder.Web.Models.Common
{
    using System.Collections.Generic;

    public class DataListingsModel<T> where T : class
    {
        public DataListingsModel(int totalCount, IEnumerable<T> data)
        {
            this.TotalCount = totalCount;
            this.Data = data;
        }

        public int TotalCount { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
