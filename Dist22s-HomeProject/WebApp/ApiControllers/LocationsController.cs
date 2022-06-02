using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Location = App.Public.DTO.v1.Location;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public LocationsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Locations
        /// <summary>
        /// Gets all the locations from the rest backend
        /// </summary>
        /// <returns>All the locations</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<Location> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            var res = (await _bll.Locations.GetAllAsync())
                .Select(x => LocationMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/Locations/5
        /// <summary>
        /// Get exactly needed you locations by id from the rest backend
        /// </summary>
        /// <param name="id">Supply location id</param>
        /// <returns>Location by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Location ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(Guid id)
        {
            var location = await _bll.Locations.FirstOrDefaultAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            var res = LocationMapper.MapFromBll(location);

            return res;
        }

        // PUT: api/Locations/5
        /// <summary>
        /// Insert new data to location found by id, edit location in the rest backend
        /// </summary>
        /// <param name="id">Supply location id</param>
        /// <param name="location">Supply location name</param>
        /// <returns>Updated location object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Location ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(Guid id, Location location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            var locationDb = await _bll.Locations.FirstOrDefaultAsync(id);
            if (locationDb == null)
            {
                return NotFound();
            }
            
            locationDb.LocationName.SetTranslation(location.LocationName);

            _bll.Locations.Update(locationDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locations
        /// <summary>
        /// Create new location in the rest backend
        /// </summary>
        /// <param name="location">Supply location name</param>
        /// <returns>New location object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Location ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {
            var locationDb = LocationMapper.MapToBll(location);
            locationDb.LocationName.SetTranslation(location.LocationName);
            _bll.Locations.Add(locationDb);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetLocation",
                new
                {
                    id = location.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                location);
        }

        // DELETE: api/Locations/5
        /// <summary>
        /// Delete location by id from the rest backend
        /// </summary>
        /// <param name="id">Supply location id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Location ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            var location = await _bll.Locations.FirstOrDefaultAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _bll.Locations.Remove(location);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(Guid id)
        {
            return _bll.Locations.Exists(id);
        }
    }
}
