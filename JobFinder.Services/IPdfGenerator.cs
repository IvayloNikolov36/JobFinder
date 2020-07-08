namespace JobFinder.Services
{
    public interface IPdfGenerator
    {
        byte[] Generate(string htmlString);
    }
}
