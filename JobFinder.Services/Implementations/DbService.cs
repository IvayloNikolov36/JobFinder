namespace JobFinder.Services.Implementations
{
    using JobFinder.Data;

    public abstract class DbService
    {
        //TODO: delete this
        protected DbService(JobFinderDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected JobFinderDbContext DbContext { get; }
    }
}
