namespace GithubSearch.Services
{
    public class HttpService    
    {
        private readonly HttpClient _httpClient;
        public HttpService()
        {
            _httpClient=new HttpClient();
        }
         public HttpClient HttpClient => _httpClient;
    }
}
