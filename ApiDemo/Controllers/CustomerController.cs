using ApiDemo.Dto;
using ApiDemo.Models;
using ApiDemo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _repository;
        private readonly IMapper _mapper;
        public CustomerController(CustomerRepository repository , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get(string? keyword)
        {
            var customers = _repository.Get(keyword);
            var customerDTOs = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            return Ok(customerDTOs);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var customer = _repository.GetById(id);
            if (customer == null)
                return NotFound();
            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            return Ok(customerDTO);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post([FromBody] Customer customerCreate)
        {
            if (customerCreate == null)
                return BadRequest();
            var customer = _repository.Get()
                .Where(c => c.CustomerId.Trim().ToUpper() == customerCreate.CustomerId.Trim().ToUpper())
                .FirstOrDefault();
            if (customer != null)
            {
                ModelState.AddModelError("", "此 Customer 已存在");
                return StatusCode(422, ModelState);
            }

            _repository.Create(customerCreate);
            return Ok("成功建立");
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Customer customerUpdate)
        {
            if (customerUpdate == null)
                return BadRequest("更新物件為空");
            if (!_repository.IsIdExist(id))
                return BadRequest("找不到物件");
            _repository.Update(customerUpdate);
            return Ok("成功更新");
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (id == null)
                return BadRequest();
            var customer = _repository.Get()
                                      .Where(c => c.CustomerId == id)
                                      .FirstOrDefault();
            if (customer == null)
                return BadRequest();
            _repository.Delete(customer);
            return Ok("成功刪除");
        }
    }
}
