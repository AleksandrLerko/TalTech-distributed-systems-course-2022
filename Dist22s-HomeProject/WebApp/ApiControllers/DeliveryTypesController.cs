using System.Text.Json;
using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeliveryType = App.Public.DTO.v1.DeliveryType;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DeliveryTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public DeliveryTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/DeliveryTypes
        /// <summary>
        /// Gets all the deliverytypes from the rest backend
        /// </summary>
        /// <returns>All the deliverytypes</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<DeliveryType> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryType>>> GetDeliveryTypes()
        {
            // var restest = (await _bll.DeliveryTypes.GetAllAsync()).ToList()[0];
            var res = (await _bll.DeliveryTypes.GetAllAsync())
                .Select(x => DeliveryTypeMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/DeliveryTypes/5
        /// <summary>
        /// Get exactly needed you deliverytype by id from the rest backend
        /// </summary>
        /// <param name="id">Supply deliveryType id</param>
        /// <returns>DeliveryType by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( DeliveryType ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryType>> GetDeliveryType(Guid id)
        {
            var deliveryType = await _bll.DeliveryTypes.FirstOrDefaultAsync(id);

            if (deliveryType == null)
            {
                return NotFound();
            }

            var res = DeliveryTypeMapper.MapFromBll(deliveryType);

            return res;
        }

        // PUT: api/DeliveryTypes/5
        /// <summary>
        /// Insert new data to deliverytype found by id, edit deliverytype in the rest backend
        /// </summary>
        /// <param name="id">Supply deliverytype id</param>
        /// <param name="deliveryType">Supply deliverytype name and comment</param>
        /// <returns>Updated deliverytype objects</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( DeliveryType ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryType(Guid id, DeliveryType deliveryType)
        {
            if (id != deliveryType.Id)
            {
                return BadRequest();
            }

            var deliveryTypeDb = await _bll.DeliveryTypes.FirstOrDefaultAsync(id);
            
            if (deliveryTypeDb == null)
            {
                return NotFound();
            }

            deliveryTypeDb.Price = deliveryType.Price;
            deliveryTypeDb.Comment = deliveryType.Comment;
            deliveryTypeDb.TypeName.SetTranslation(deliveryType.TypeName);
            _bll.DeliveryTypes.Update(deliveryTypeDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryTypeExists(id))
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

        // POST: api/DeliveryTypes
        /// <summary>
        /// Create new deliverytype object in the rest backend
        /// </summary>
        /// <param name="deliveryType">Supply deliverytype name and comment</param>
        /// <returns>New deliverytype object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( DeliveryType ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<DeliveryType>> PostDeliveryType(DeliveryType deliveryType)
        {
            var deliveryTypeDb = DeliveryTypeMapper.MapToBll(deliveryType);
            deliveryTypeDb.TypeName.SetTranslation(deliveryType.TypeName);
            _bll.DeliveryTypes.Add(deliveryTypeDb);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetDeliveryType",
                new
                {
                    id = deliveryType.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                deliveryType);
        }

        // DELETE: api/DeliveryTypes/5
        /// <summary>
        /// Delete deliverytype object by id from the rest backend
        /// </summary>
        /// <param name="id">Supply deliverytype id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( DeliveryType ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryType(Guid id)
        {
            var deliveryType = await _bll.DeliveryTypes.FirstOrDefaultAsync(id);
            if (deliveryType == null)
            {
                return NotFound();
            }

            _bll.DeliveryTypes.Remove(deliveryType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryTypeExists(Guid id)
        {
            return _bll.DeliveryTypes.Exists(id);
        }
    }
}
