using GithubSearch.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace GithubSearch
{
    [Route("api/[controller]")]
    [ApiController]
    public class FindController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly HttpContext _httpContext;
        public FindController(DataContext dataContext, HttpContext httpContext)
        {
            _dataContext = dataContext;
            _httpContext = httpContext;
        }
        // GET: api/<FindController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FindController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FindController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            
        }

        // PUT api/<FindController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FindController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
