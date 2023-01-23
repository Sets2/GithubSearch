using System.Text;

namespace GithubSearch.Services
{
    public class GitSearch: IGitSearch
    {
        const string uri = "https://api.github.com/search/repositories?q=";
        const string uagent = "Mozilla/5.0 (Windows NT 10.0; Win64; rv:77.0) Gecko/20100101 Firefox/77.0";
        ILogger<GitSearch> _logger;
        HttpService _httpService;
        public GitSearch(ILogger<GitSearch> logger, HttpService httpService)
        {
            _logger = logger;
            _httpService = httpService;
        }

        public async Task<string?> GetSearch(string searchStr)
        {
             string? content = null;
            HttpRequestMessage? request = null;
            HttpResponseMessage? response = null;
            try
            {
                request = new HttpRequestMessage(HttpMethod.Get, uri + searchStr);
                request.Headers.Add("Accept-Charset", "UTF-8");
                request.Headers.Add("User-Agent", uagent);
                response = await _httpService.HttpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                if (response.IsSuccessStatusCode)
                {
                    var contentByte = await response.Content.ReadAsByteArrayAsync();
                    if (response.Content.Headers.ContentType?.CharSet?.Contains("1251") == true)
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        content = Encoding.GetEncoding(1251).GetString(contentByte);
                    }
                    else content = Encoding.UTF8.GetString(contentByte);
                }
            }
                catch (Exception e)
        {
            _logger.LogError(e, $"{e.Message} Возникла ошибка при получении данных с Git по строке {searchStr}");
        }
        finally
        {
            response?.Dispose();
            request?.Dispose();
        }
        return content;
    }        
    }
}
