namespace JobFinder.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IJobCategoriesService
    {
        Task<IEnumerable<T>> AllAsync<T>();
    }
}
