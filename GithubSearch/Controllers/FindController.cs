using GithubSearch.Core.Domain;
using GithubSearch.DataAccess;
using GithubSearch.Mappers;
using GithubSearch.Models;
using GithubSearch.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GithubSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FindController : ControllerBase
    {
        private const int _defPageSize = 20;
        private readonly ILogger<FindController> _logger;
        private readonly DataContext _dataContext;
        private readonly IGitSearch _gitSearch;
        public FindController(ILogger<FindController> logger, DataContext dataContext, IGitSearch gitSearch)
        {
            _logger = logger;
            _dataContext = dataContext;
            _gitSearch = gitSearch;
        }
        // GET: api/<FindController>
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] int? n, [FromQuery] int? s)
        {
            List<FindResponse> result = new();
            try
            {
                var items = await _dataContext.GitResponse.ToListAsync();
                if (items!=null) 
                { 
                    foreach(var item in items)
                    {
                        var itemResult = GitResponseToGet.MapFromModel(item);
                        result.AddRange(itemResult);
                    }
                }
                var pag = new Pagination();
                pag.pageNumber = n ?? 1;
                pag.pageSize = s ?? _defPageSize;
                pag.totalCount = result.Count;
                pag.totalGitResponse = items?.Count ?? 0;
                var count = pag.pageNumber * pag.pageSize > pag.totalCount ?
                    pag.totalCount % pag.pageSize : pag.pageSize;
                result = result.GetRange((pag.pageNumber - 1)*pag.pageSize
                    , count);
                pag.totalPages = pag.totalCount / pag.pageSize + (count>0? 1:0);

                var resultPag = new FindResponseWithPagination() 
                    { Response = result, Pagination = pag };
                return Ok(resultPag);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{e.Message} Ошибка обращения к БД");
                return Problem("Ошибка обращения к БД");
            }
        }

        // POST api/<FindController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string value)
        {
            var result = await _gitSearch.GetSearch(value);
            if (result is string)
            {
                try
                {
                    var item = new GitResponse() { SearchString = value, SearchResult = result };
                    await _dataContext.AddAsync(item);
                    await _dataContext.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"{e.Message} Ошибка обращения к БД");
                    return Problem("Ошибка обращения к БД");
                }
            }
            return Problem("Ошибка обращения к Git");
        }

        // DELETE api/<FindController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _dataContext.GitResponse.FirstOrDefaultAsync(x => x.Id == id);
                if (result == null) return NotFound();
                _dataContext.GitResponse.Remove(result);
                await _dataContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                var err = "Ошибка обновления данных в таблице channel БД";
                _logger.LogError(e, $"{e.Message}. {err}");
                return Problem(err);
            }
        }
    }
}
