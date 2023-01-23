using GithubSearch.DataAccess;
using GithubSearch.Services;
using Microsoft.AspNetCore.Mvc;

namespace GithubSearch
{
    [Route("api/[controller]")]
    [ApiController]
    public class FindController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IGitSearch _gitSearch;
        public FindController(DataContext dataContext, IGitSearch gitSearch)
        {
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
            return Ok(result);
        }

        // DELETE api/<FindController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return null;
        }
    }
}
