using GithubSearch.Core.Domain;
using GithubSearch.DataAccess;
using GithubSearch.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
            return null;
        }
    }
}
