using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InStock = App.Public.DTO.v1.InStock;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class InStocksController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public InStocksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/InStocks
        /// <summary>
        /// Gets all the InStock objects from the rest backend
        /// </summary>
        /// <returns>All the InStock objects</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<InStock> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InStock>>> GetInStocks()
        {
            var res = (await _bll.InStocks.GetAllAsync())
                .Select(x => InStockMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/InStocks/5
        /// <summary>
        /// Get exactly needed you InStock object by id from the rest backend
        /// </summary>
        /// <param name="id">Supply InStock id</param>
        /// <returns>InStock by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( InStock ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<InStock>> GetInStock(Guid id)
        {
            var inStock = await _bll.InStocks.FirstOrDefaultAsync(id);

            if (inStock == null)
            {
                return NotFound();
            }

            var res = InStockMapper.MapFromBll(inStock);

            return res;
        }

        // PUT: api/InStocks/5
        /// <summary>
        /// Insert new data to InStock object found by id, edit InStock object in the rest backend
        /// </summary>
        /// <param name="id">Supply InStock id</param>
        /// <param name="inStock">Supply InStock quantity</param>
        /// <returns>Updated InStock object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( InStock ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInStock(Guid id, InStock inStock)
        {
            if (id != inStock.Id)
            {
                return BadRequest();
            }

            var inStockDb = await _bll.InStocks.FirstOrDefaultAsync(id);
            if (inStockDb == null)
            {
                return NotFound();
            }

            inStockDb.Quantity = inStock.Quantity;

            _bll.InStocks.Update(inStockDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InStockExists(id))
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

        // POST: api/InStocks
        /// <summary>
        /// Create new InStock object in the rest backend
        /// </summary>
        /// <param name="inStock">Supply InStock quantity</param>
        /// <returns>New InStock object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( InStock ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<InStock>> PostInStock(InStock inStock)
        {
            var inStockDb = InStockMapper.MapToBll(inStock);
            
            _bll.InStocks.Add(inStockDb);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetInStock",
                new
                {
                    id = inStock.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                inStock);
        }

        // DELETE: api/InStocks/5
        /// <summary>
        /// Delete InStock object by id from the rest backend
        /// </summary>
        /// <param name="id">Supply InStock id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( InStock ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInStock(Guid id)
        {
            var inStock = await _bll.InStocks.FirstOrDefaultAsync(id);
            if (inStock == null)
            {
                return NotFound();
            }

            _bll.InStocks.Remove(inStock);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool InStockExists(Guid id)
        {
            return _bll.InStocks.Exists(id);
        }
    }
}
