namespace GithubSearch.Models
{
    public class FindResponse
    {
        public long Id { get; set; }
        public string SearchString { get; set; } = null!;
        public string? ProjectName { get; set; }
        public int? StargazersCount { get; set; }
        public int? WatchersCount { get; set; }
        public string? ProjectUrl { get; set; }
        public string? Owner { get; set; }
    }

    public class FindResponseWithPagination
    {
        public List<FindResponse> Response { get; set; } = null!;
        public Pagination Pagination { get; set; } = null!;
    }
}
