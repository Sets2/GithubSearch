using System.Text.Json.Serialization;

namespace GithubSearch.Models
{
    public class GitResult
    {
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
        [JsonPropertyName("items")]
        public Item[] Item { get; set; } = null!;
    }

    public class Item
    {
        [JsonPropertyName("name")]
        public string? ProjectName { get; set; }
        [JsonPropertyName("stargazers_count")]
        public string? StargazersCount { get; set; }
        [JsonPropertyName("watchers_count")]
        public string? WatchersCount { get; set; }
        [JsonPropertyName("html_url")]
        public string? ProjectUrl { get; set; }
        [JsonPropertyName("owner")]
        public Owner? Owner { get; set; }
    }
    public class Owner 
    {
        [JsonPropertyName("login")]
        public string? Login { get; set; }
    }
}
