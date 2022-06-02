using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Customer = App.Public.DTO.v1.Customer;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CustomersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Customers
        /// <summary>
        /// Gets all the customers from the rest backend
        /// </summary>
        /// <returns>All the customers</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<Customer> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var res = (await _bll.Customers.GetAllAsync())
                .Select(x => CustomerMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/Customers/5
        /// <summary>
        /// Get exactly needed you customer by id from the rest backend
        /// </summary>
        /// <param name="id">Supply customer id</param>
        /// <returns>Customer by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Customer ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _bll.Customers.FirstOrDefaultAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var res = CustomerMapper.MapFromBll(customer);
            return res;
        }

        // PUT: api/Customers/5
        /// <summary>
        /// Insert new data to customer found by id, edit customer in the rest backend
        /// </summary>
        /// <param name="id">Supply customer id</param>
        /// <param name="customer">Supply firstname, lastname, email and phone number</param>
        /// <returns>Updated customer object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Customer ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
            var customerDb = await _bll.Customers.FirstOrDefaultAsync(id);
            if (customerDb == null)
            {
                return NotFound();
            }

            customerDb.FirstName = customer.FirstName;
            customerDb.LastName = customer.LastName;
            customerDb.Email = customer.Email;
            customerDb.PhoneNumber = customer.PhoneNumber;

            _bll.Customers.Update(customerDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        /// <summary>
        /// Create new customer object in the rest backend
        /// </summary>
        /// <param name="customer">Supply firstname, lastname, email and phone number</param>
        /// <returns>New customer object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Customer ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var customerDb = CustomerMapper.MapToBll(customer);
            _bll.Customers.Add(customerDb);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetCustomer",
                new
                {
                    id = customer.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                customer);
        }

        // DELETE: api/Customers/5
        /// <summary>
        /// Delete customer object by id from the rest backend
        /// </summary>
        /// <param name="id">Supply customer id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Customer ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _bll.Customers.FirstOrDefaultAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _bll.Customers.Remove(customer);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(Guid id)
        {
            return _bll.Customers.Exists(id);
        }
    }
}
