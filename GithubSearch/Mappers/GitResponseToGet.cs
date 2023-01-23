using GithubSearch.Core.Domain;
using GithubSearch.Models;
using System.Text.Json;

namespace GithubSearch.Mappers
{
    public static class GitResponseToGet
    {
        public static FindResponse MapFromModel(GitResponse item)
        {
            FindResponse result = null;
            try
            {
                var deSerResult = JsonSerializer.Deserialize<GitResponse>(item.SearchResult);

                result = new FindResponse()
                {

                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при десериализации");
            }
            return result;
        }
    }
}
