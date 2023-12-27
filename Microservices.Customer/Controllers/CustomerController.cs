using Microservices.Customer.Models;
using Microservices.Customer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericRepository<Customers> _customerRepository;
       
        public CustomerController(IGenericRepository<Customers> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Customers>>> GetAllCustomers()
        {
            return Ok(await _customerRepository.GetAllAsync());
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Customers>> GetCustomerById(int id)
        {
            return Ok(await _customerRepository.GetByIdAsync(id));
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCustomer([FromBody] Customers customer)
        {
            var result = _customerRepository.Add(customer);
            await _customerRepository.SaveChangesAsync();
            return Ok(result);
        }

        //logic for update customer
        [HttpPut]
        [Route("")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customers customer)
        {
            //code to update exisiting entries by searching Id and do not add as new record
            var result = _customerRepository.Update(customer);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            var result = _customerRepository.Delete(customer);
            return Ok(result);
        }
    }
}
