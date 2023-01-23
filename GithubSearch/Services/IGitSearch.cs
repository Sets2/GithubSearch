namespace GithubSearch.Services
{
    public interface IGitSearch
    {
        public Task<string?> GetSearch(string searchStr);
    }
}
