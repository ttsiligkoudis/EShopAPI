using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repositories.Customers;
using DataModels.Dtos;
using DataModels;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Get a list of Customers
        /// </summary>
        /// <returns></returns>
        // GET: api/Customers
        [HttpGet(Name = "GetCustomers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _customerRepository.GetCustomers();
            if (!customers.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));

        }

        /// <summary>
        /// Get a specific customer by id
        /// </summary>
        /// <param name="id"> The customer's id</param>
        /// <returns></returns>
        // GET: api/Customers/id
        [HttpGet("{id}", Name = "GetCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        /// <summary>
        /// Get a specific customer by user id
        /// </summary>
        /// <param name="id"> The customer's user id</param>
        /// <returns></returns>
        // GET: api/Customers/User/id
        [HttpGet("User/{id}", Name = "GetCustomerByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDto>> GetCustomerByUserId(int id)
        {
            var customer = await _customerRepository.GetCustomerByUserId(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        /// <summary>
        /// Update a specific customer
        /// </summary>
        /// <param name="id"> The customer's id</param>
        /// <param name="customerDto"> The instance of the customer</param>
        /// <returns></returns>
        // PUT: api/Customers/id
        [HttpPut("{id}", Name = "PutCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutCustomer(int id, CustomerDto customerDto)
        {
            var customerInDb = await _customerRepository.GetCustomer(customerDto.Id);
            if (customerInDb == null || id != customerDto.Id)
            {
                return NotFound();
            }
            _mapper.Map(customerDto, customerInDb);
            await _customerRepository.Put(customerInDb);
            return NoContent();
        }

        /// <summary>
        /// Create a customer
        /// </summary>
        /// <param name="customerDto"> An instance of a customer</param>
        /// <returns></returns>
        // POST: api/Customers
        [HttpPost(Name = "PostCustomer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CustomerDto>> PostCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer.RegDate = DateTime.Now;
            customer = await _customerRepository.Post(customer);
            _mapper.Map(customer, customerDto);
            return CreatedAtAction("GetCustomer", new { id = customerDto.Id }, customerDto);
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="id"> The customer's id</param>
        /// <returns></returns>
        // DELETE: api/Customers/id
        [HttpDelete("{id}", Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            await _customerRepository.Delete(id);
            return NoContent();
        }
    }
}
