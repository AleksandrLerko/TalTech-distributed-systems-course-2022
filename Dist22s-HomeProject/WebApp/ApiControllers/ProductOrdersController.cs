#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using App.Public.Mappers;
using ProductOrders = App.Public.DTO.v1.ProductOrders;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductOrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ProductOrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ProductOrders
        /// <summary>
        /// Get all the ProductOrders from the rest backend
        /// </summary>
        /// <returns>All the ProductOrders</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<ProductOrders> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductOrders>>> GetProductOrders()
        {
            var res = (await _bll.ProductOrders.GetAllAsync())
                .Select(x => ProductOrdersMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/ProductOrders/5
        /// <summary>
        /// Get ProductOrder by id
        /// </summary>
        /// <param name="id">Supply ProductOrders id</param>
        /// <returns>ProductOrders by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( ProductOrders ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOrders>> GetProductOrders(Guid id)
        {
            var productOrders = await _bll.ProductOrders.FirstOrDefaultAsync(id);

            if (productOrders == null)
            {
                return NotFound();
            }

            return ProductOrdersMapper.MapFromBll(productOrders);
        }

        // PUT: api/ProductOrders/5
        /// <summary>
        /// Get exactly needed you ProductOrders object by id from the rest backend
        /// </summary>
        /// <param name="id">Supply ProductOrders id</param>
        /// <param name="productOrders">Supply order and product id's</param>
        /// <returns>Updated ProductOrders object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( ProductOrders ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductOrders(Guid id, ProductOrders productOrders)
        {
            if (id != productOrders.Id)
            {
                return BadRequest();
            }

            var productOrderDb = await _bll.ProductOrders.FirstOrDefaultAsync(id);

            if (productOrderDb == null)
            {
                return NotFound();
            }

            _bll.ProductOrders.Update(productOrderDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductOrdersExists(id))
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

        // POST: api/ProductOrders
        /// <summary>
        /// Create new ProductOrders object
        /// </summary>
        /// <param name="productOrders">Supply product and order id's</param>
        /// <returns>New ProductOrder object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( ProductOrders ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<ProductOrders>> PostProductOrders(ProductOrders productOrders)
        {
            _bll.ProductOrders.Add(ProductOrdersMapper.MapToBll(productOrders));
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetProductOrders",
                new
                {
                    id = productOrders.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                productOrders);
        }

        // DELETE: api/ProductOrders/5
        /// <summary>
        /// Delete ProductOrders by id from the rest backend
        /// </summary>
        /// <param name="id">Supply ProductOrders id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( ProductOrders ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductOrders(Guid id)
        {
            var productOrders = await _bll.ProductOrders.FirstOrDefaultAsync(id);
            if (productOrders == null)
            {
                return NotFound();
            }

            _bll.ProductOrders.Remove(productOrders);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductOrdersExists(Guid id)
        {
            return _bll.ProductOrders.Exists(id);
        }
    }
}
