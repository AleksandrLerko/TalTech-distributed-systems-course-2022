using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ShippingInfoAppUser = App.Public.DTO.v1.ShippingInfoAppUser;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShippingInfoAppUsersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        public ShippingInfoAppUsersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ShippingInfoAppUsers
        /// <summary>
        /// Gets all the ShippingInfoAppUsers from the rest backend
        /// </summary>
        /// <returns>All the ShippingInfoAppUsers</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<ShippingInfoAppUser> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingInfoAppUser>>> GetShippingInfoAppUsers()
        {
            Console.WriteLine(User.GetUserId());
            var res = (await _bll.ShippingInfoAppUsers.GetAllAsync(User.GetUserId()))
                .Select(x => ShippingInfoAppUserMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/ShippingInfoAppUsers/5
        /// <summary>
        /// Get exactly needed you ShippingInfoAppUsers by id from the rest backend 
        /// </summary>
        /// <param name="id">Supply ShippingInfoAppUsers id</param>
        /// <returns>ShippingInfoAppUsers by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( ShippingInfoAppUser ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingInfoAppUser>> GetShippingInfoAppUser(Guid id)
        {
            var shippingInfoAppUser = await _bll.ShippingInfoAppUsers.FirstOrDefaultAsync(id);

            if (shippingInfoAppUser == null)
            {
                return NotFound();
            }

            var res = ShippingInfoAppUserMapper.MapFromBll(shippingInfoAppUser);
            return res;
        }

        // PUT: api/ShippingInfoAppUsers/5
        /// <summary>
        /// Insert new data to ShippingInfoAppUsers found by id, edit ShippingInfoAppUsers in the rest backend
        /// </summary>
        /// <param name="id">Supply ShippingInfoAppUsers id</param>
        /// <param name="shippingInfoAppUser">Supply ShippingInfo and AppUsers id</param>
        /// <returns>Updated ShippingInfoAppUsers object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( ShippingInfoAppUser ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippingInfoAppUser(Guid id, ShippingInfoAppUser shippingInfoAppUser)
        {
            if (id != shippingInfoAppUser.Id)
            {
                return BadRequest();
            }

            var shippingInfoAppUserDb = await _bll.ShippingInfoAppUsers.FirstOrDefaultAsync(id);
            if (shippingInfoAppUserDb == null)
            {
                return NotFound();
            }
            
            _bll.ShippingInfoAppUsers.Update(shippingInfoAppUserDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingInfoAppUserExists(id))
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

        // POST: api/ShippingInfoAppUsers
        /// <summary>
        /// Create new ShippingInfoAppUsers object
        /// </summary>
        /// <param name="shippingInfoAppUser">Supply ShippingInfo or AppUsers id</param>
        /// <returns>New ShippingInfoAppUsers object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( ShippingInfoAppUser ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<ShippingInfoAppUser>> PostShippingInfoAppUser(ShippingInfoAppUser shippingInfoAppUser)
        {
            var res = ShippingInfoAppUserMapper.MapToBll(shippingInfoAppUser);
            _bll.ShippingInfoAppUsers.Add(res);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetShippingInfoAppUser",
                new
                {
                    id = shippingInfoAppUser.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                shippingInfoAppUser);
        }

        // DELETE: api/ShippingInfoAppUsers/5
        /// <summary>
        /// Delete ShippingInfoAppUsers by id
        /// </summary>
        /// <param name="id">Supply ShippingInfoAppUsers id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( ShippingInfoAppUser ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingInfoAppUser(Guid id)
        {
            var shippingInfoAppUser = await _bll.ShippingInfoAppUsers.FirstOrDefaultAsync(id);
            if (shippingInfoAppUser == null)
            {
                return NotFound();
            }

            _bll.ShippingInfoAppUsers.Remove(shippingInfoAppUser);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ShippingInfoAppUserExists(Guid id)
        {
            return _bll.ShippingInfoAppUsers.Exists(id);
        }
    }
}
