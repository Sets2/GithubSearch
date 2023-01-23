namespace GithubSearch.Services
{
    public class HttpContext
    {
        private readonly HttpClient _httpClient;
        public HttpContext()
        {
            _httpClient=new HttpClient();
        }
         public HttpClient HttpClient => _httpClient;
    }
}
