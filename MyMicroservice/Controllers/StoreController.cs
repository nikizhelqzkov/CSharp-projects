using Microsoft.AspNetCore.Mvc;
using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.Db;
using MyMicroservice.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyMicroservice.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        // GET: api/<ValuesController>
        private readonly IStoreService _service;
        public StoreController(IStoreService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<List<Store>> Get()
        {
            var result = _service.GetStores();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
