using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Order = App.Public.DTO.v1.Order;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Orders
        /// <summary>
        /// Gets all the orders from the rest backend
        /// </summary>
        /// <returns>All the orders</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<Order> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var res = (await _bll.Orders.GetAllAsync(User.GetUserId()))
                .Select(x => OrderMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/Orders/5
        /// <summary>
        /// Get exactly needed you order by id from the rest backend
        /// </summary>
        /// <param name="id">Supply order by id</param>
        /// <returns>Order by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Order ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var res = OrderMapper.MapFromBll(order);

            return res;
        }

        // PUT: api/Orders/5
        /// <summary>
        /// Insert new data to order found by id, edit order in the rest backend
        /// </summary>
        /// <param name="id">Supply order id</param>
        /// <param name="order">Supply all the id's</param>
        /// <returns>Updated order object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Order ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            var orderDb = await _bll.Orders.FirstOrDefaultAsync(id);
            if (orderDb == null)
            {
                return NotFound();
            }
            
            _bll.Orders.Update(orderDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        /// <summary>
        /// Create new order in the rest 
        /// </summary>
        /// <param name="order">Supply order all the id's</param>
        /// <returns>New orders object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Order ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            var orderDb = OrderMapper.MapToBll(order);
            _bll.Orders.Add(orderDb);
            await _bll.SaveChangesAsync();
            
            

            return CreatedAtAction(
                "GetOrder",
                new
                {
                    id = order.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                order);
        }

        // DELETE: api/Orders/5
        /// <summary>
        /// Delete orders by id from the rest backend
        /// </summary>
        /// <param name="id">Supply order id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Order ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _bll.Orders.Remove(order);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(Guid id)
        {
            return _bll.Orders.Exists(id);
        }
    }
}
