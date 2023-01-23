using GithubSearch.DataAccess;
using GithubSearch.Mappers;
using GithubSearch.Models;
using GithubSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GithubSearch.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DataContext _dataContext;
        private readonly IGitSearch _gitSearch;

        public IndexModel(ILogger<IndexModel> logger, DataContext dataContext, IGitSearch gitSearch)
        {
            _logger = logger;
            _dataContext = dataContext;
            _gitSearch = gitSearch;
        }
        public List<FindResponse> Result { get; private set; } = new();
        public string TempString()
        {
            if (Result.Count > 0) return JsonSerializer.Serialize(Result);
            else return "";
        }

        public void OnGet()
        {

        }
        public async Task OnPostAsync(string search)
        {
            Result.Clear();
            try
            {
                var items = await _dataContext.GitResponse.
                    Where(x=> EF.Functions.
                    Like(x.SearchString!, $"%{search}%")).ToListAsync();
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        var itemResult = GitResponseToGet.MapFromModel(item);
                        Result.AddRange(itemResult);
                    }
                }
             }
            catch (Exception e)
            {
                _logger.LogError(e, $"{e.Message} Ошибка обращения к БД");

            }
        }
    }
}