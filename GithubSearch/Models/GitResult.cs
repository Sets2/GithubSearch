using System.Text.Json.Serialization;

namespace GithubSearch.Models
{
    public class GitResult
    {
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
        [JsonPropertyName("items")]
        public Item[] Items { get; set; } = null!;
    }

    public class Item
    {
        [JsonPropertyName("name")]
        public string? ProjectName { get; set; }
        [JsonPropertyName("stargazers_count")]
        public int? StargazersCount { get; set; }
        [JsonPropertyName("watchers_count")]
        public int? WatchersCount { get; set; }
        [JsonPropertyName("html_url")]
        public string? ProjectUrl { get; set; }
        [JsonPropertyName("owner")]
        public Owner Owner { get; set; } = null!;
    }
    public class Owner 
    {
        [JsonPropertyName("login")]
        public string? Login { get; set; }
    }
}
