using GithubSearch.Core.Domain;
using GithubSearch.DataAccess;
using GithubSearch.Mappers;
using GithubSearch.Models;
using GithubSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GithubSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FindController : ControllerBase
    {
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
        public async Task<ActionResult> Get()
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
                return Ok(result);
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
