using GithubSearch.Core.Domain;
using GithubSearch.Models;
using System.Text.Json;

namespace GithubSearch.Mappers
{
    public static class GitResponseToGet
    {
        public static List<FindResponse> MapFromModel(GitResponse item)
        {
            List<FindResponse> result = new ();
            try
            {
                var deSerResult = JsonSerializer.Deserialize<GitResult>(item.SearchResult);
                if (deSerResult != null)
                {
                    foreach(var deser in deSerResult.Items) 
                    {
                        var resultItem = new FindResponse();
                        resultItem.ProjectName = deser.ProjectName;
                        resultItem.ProjectUrl = deser.ProjectUrl;
                        resultItem.WatchersCount = deser.WatchersCount;
                        resultItem.StargazersCount = deser.StargazersCount;
                        resultItem.Owner = deser.Owner.Login;
                        resultItem.Id = item.Id;
                        resultItem.SearchString = item.SearchString;
                    
                        result.Add(resultItem);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} Ошибка при десериализации");
            }
            return result;
        }
    }
}
