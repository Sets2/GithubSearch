using System.Text.Json.Serialization;

namespace GithubSearch.Models
{
    public class SearchResultDto
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
