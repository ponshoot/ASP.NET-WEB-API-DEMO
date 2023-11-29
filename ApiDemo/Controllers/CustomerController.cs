using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CustomerController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Customers.ToList());
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (!_context.Customers.Where(c => c.CustomerId == id).Any())
                return NotFound();
            return Ok(_context.Customers.Where(c => c.CustomerId == id).FirstOrDefault());
        }

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
