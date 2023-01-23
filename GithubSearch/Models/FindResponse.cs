namespace GithubSearch.Models
{
    public class FindResponse
    {
        public long Id { get; set; }
        public string SearchString { get; set; } = null!;
        public string? ProjectName { get; set; }
        public string? StargazersCount { get; set; }
        public string? WatchersCount { get; set; }
        public string? ProjectUrl { get; set; }
        public string? Owner { get; set; }
    }
}
