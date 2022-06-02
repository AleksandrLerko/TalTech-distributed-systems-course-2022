using App.Contracts.BLL;
using App.Public.DTO.v1;
using App.Public.Mappers;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ShippingInfo = App.Public.DTO.v1.ShippingInfo;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShippingInfosController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ShippingInfosController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ShippingInfos
        /// <summary>
        /// Gets all the ShippingInfos from the rest backend
        /// </summary>
        /// <returns>All the ShippingInfos</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<ShippingInfo> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingInfo>>> GetShippingInfos()
        {
            var res = (await _bll.ShippingInfos.GetAllAsync())
                .Select(x => ShippingInfoMapper.MapFromBll(x)).ToList(); 
            return res;
        }

        // GET: api/ShippingInfos/5
        /// <summary>
        /// Get exactly needed you ShippingInfos by id from the rest backend
        /// </summary>
        /// <param name="id">Supply ShippingInfos id</param>
        /// <returns>ShippingInfos by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( ShippingInfo ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingInfo>> GetShippingInfo(Guid id)
        {
            var shippingInfo = await _bll.ShippingInfos.FirstOrDefaultAsync(id);

            if (shippingInfo == null)
            {
                return NotFound();
            }

            var res = ShippingInfoMapper.MapFromBll(shippingInfo);
            return res;
        }

        // PUT: api/ShippingInfos/5
        /// <summary>
        /// Insert new data to ShippingInfos found by id, edit ShippingInfos in the rest backend
        /// </summary>
        /// <param name="id">Supply ShippingInfos id</param>
        /// <param name="shippingInfo">Supply address one, address two and deliverytype id</param>
        /// <returns>Updated ShippingInfos object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( ShippingInfo ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippingInfo(Guid id, ShippingInfo shippingInfo)
        {
            if (id != shippingInfo.Id)
            {
                return BadRequest();
            }

            var shippingInfoDb = await _bll.ShippingInfos.FirstOrDefaultAsync(id);
            if (shippingInfoDb == null)
            {
                return NotFound();
            }

            shippingInfoDb.AddressOne = shippingInfo.AddressOne;
            shippingInfoDb.AddressTwo = shippingInfo.AddressTwo;
            
            _bll.ShippingInfos.Update(shippingInfoDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingInfoExists(id))
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

        // POST: api/ShippingInfos
        /// <summary>
        /// Create new ShippingInfos object
        /// </summary>
        /// <param name="shippingInfo">Supply address one, address two and deliverytype id</param>
        /// <returns>New ShippingInfos object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( ShippingInfo ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<ShippingInfo>> PostShippingInfo(ShippingInfo shippingInfo)
        {
            var res = ShippingInfoMapper.MapToBll(shippingInfo);
            // res.Id = new Guid();
            _bll.ShippingInfos.Add(res);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetShippingInfo",
                new
                {
                    id = shippingInfo.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                shippingInfo);
        }

        // DELETE: api/ShippingInfos/5
        /// <summary>
        /// Delete ShippingInfos by id
        /// </summary>
        /// <param name="id">Supply ShippingInfos id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( ShippingInfo ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingInfo(Guid id)
        {
            var shippingInfo = await _bll.ShippingInfos.FirstOrDefaultAsync(id);
            if (shippingInfo == null)
            {
                return NotFound();
            }

            _bll.ShippingInfos.Remove(shippingInfo);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ShippingInfoExists(Guid id)
        {
            return _bll.ShippingInfos.Exists(id);
        }
    }
}
