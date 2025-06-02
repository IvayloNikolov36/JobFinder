namespace JobFinder.Transfer.DTOs;

public class DataListingDTO<T> where T : class
{
    public DataListingDTO(int totalCount, IEnumerable<T> data)
    {
        this.TotalCount = totalCount;
        this.Data = data;
    }

    public int TotalCount { get; set; }

    public IEnumerable<T> Data { get; set; }
}
