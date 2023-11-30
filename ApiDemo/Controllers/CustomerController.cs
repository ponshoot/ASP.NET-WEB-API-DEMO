using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Post([FromBody] Customer customerCreate)
        {
            if (customerCreate == null)
                return BadRequest();
            var customer = _context.Customers
                .Where(c => c.CustomerId.Trim().ToUpper() == customerCreate.CustomerId.Trim().ToUpper())
                .FirstOrDefault();
            if (customer != null)
            {
                ModelState.AddModelError("", "此 Customer 已存在");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Customers.Add(customerCreate);
            _context.SaveChanges();
            return Ok("成功建立");
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Customer customerUpdate)
        {
            if (customerUpdate == null)
                return BadRequest();
            if (!_context.Customers.Where(c => c.CustomerId == id).Any())
                return BadRequest();
            _context.Customers.Update(customerUpdate);
            _context.SaveChanges();
            return Ok("成功更新");
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (id == null)
                return BadRequest();
            var customer = _context.Customers.Find(id);
            if (customer == null)
                return BadRequest();
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return Ok("成功刪除");
        }
    }
}
