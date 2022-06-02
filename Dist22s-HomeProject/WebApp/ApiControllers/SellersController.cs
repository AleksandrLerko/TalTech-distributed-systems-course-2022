using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seller = App.Public.DTO.v1.Seller;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public SellersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Sellers
        /// <summary>
        /// Gets all the sellers from the rest backend
        /// </summary>
        /// <returns>All the sellers</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<Seller> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seller>>> GetSellers()
        {
            var res = (await _bll.Sellers.GetAllAsync())
                .Select(x => SellerMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/Sellers/5
        /// <summary>
        /// Get exactly needed you seller by id from the rest backend
        /// </summary>
        /// <param name="id">Supply seller id</param>
        /// <returns>Seller by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Seller ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<Seller>> GetSeller(Guid id)
        {
            var seller = await _bll.Sellers.FirstOrDefaultAsync(id);

            if (seller == null)
            {
                return NotFound();
            }

            var res = SellerMapper.MapFromBll(seller);

            return res;
        }

        // PUT: api/Sellers/5
        /// <summary>
        /// Insert new data to seller found by id, edit seller in the rest backend
        /// </summary>
        /// <param name="id">Supply seller id</param>
        /// <param name="seller">Supply seller name</param>
        /// <returns>Updated seller object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Seller ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeller(Guid id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }

            var sellerDb = await _bll.Sellers.FirstOrDefaultAsync(id);
            if (sellerDb == null)
            {
                return NotFound();
            }

            sellerDb.SellerName = seller.SellerName;
            _bll.Sellers.Update(sellerDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellerExists(id))
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

        // POST: api/Sellers
        /// <summary>
        /// Create new seller object
        /// </summary>
        /// <param name="seller">Supply seller name</param>
        /// <returns>New seller object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Seller ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<Seller>> PostSeller(Seller seller)
        {
            var sellerDb = SellerMapper.MapToBll(seller);
            _bll.Sellers.Add(sellerDb);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetSeller",
                new
                {
                    id = seller.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                seller);
        }

        // DELETE: api/Sellers/5
        /// <summary>
        /// Delete seller by id
        /// </summary>
        /// <param name="id">Supply seller id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Seller ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeller(Guid id)
        {
            var seller = await _bll.Sellers.FirstOrDefaultAsync(id);
            if (seller == null)
            {
                return NotFound();
            }

            _bll.Sellers.Remove(seller);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool SellerExists(Guid id)
        {
            return _bll.Sellers.Exists(id);
        }
    }
}
