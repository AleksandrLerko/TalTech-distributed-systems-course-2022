using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecificationType = App.Public.DTO.v1.SpecificationType;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SpecificationTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public SpecificationTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/SpecificationTypes
        /// <summary>
        /// Gets all the SpecificationTypes from the rest backend
        /// </summary>
        /// <returns>All the SpecificationTypes</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<SpecificationType> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecificationType>>> GetSpecificationTypes()
        {
            var res = (await _bll.SpecificationTypes.GetAllAsync())
                .Select(x => SpecificationTypeMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/SpecificationTypes/5
        /// <summary>
        /// Get exactly needed you SpecificationType by id from the rest backend
        /// </summary>
        /// <param name="id">Supply SpecificationTypes id</param>
        /// <returns>SpecificationTypes by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( SpecificationType ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecificationType>> GetSpecificationType(Guid id)
        {
            var specificationType = await _bll.SpecificationTypes.FirstOrDefaultAsync(id);

            if (specificationType == null)
            {
                return NotFound();
            }

            var res = SpecificationTypeMapper.MapFromBll(specificationType);

            return res;
        }

        // PUT: api/SpecificationTypes/5
        /// <summary>
        /// Insert new data to SpecificationTypes found by id, edit SpecificationTypes in the rest backend
        /// </summary>
        /// <param name="id">Supply SpecificationTypes id</param>
        /// <param name="specificationType">Supply TypeName, TypeValue and Specification id</param>
        /// <returns>Updated SpecificationTypes</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( SpecificationType ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecificationType(Guid id, SpecificationType specificationType)
        {
            if (id != specificationType.Id)
            {
                return BadRequest();
            }

            var specificationTypeDb = await _bll.SpecificationTypes.FirstOrDefaultAsync(id);
            if (specificationTypeDb == null)
            {
                return NotFound();
            }
            
            specificationTypeDb.TypeName.SetTranslation(specificationType.TypeName);
            _bll.SpecificationTypes.Update(specificationTypeDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecificationTypeExists(id))
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

        // POST: api/SpecificationTypes
        /// <summary>
        /// Create new SpecificationTypes
        /// </summary>
        /// <param name="specificationType">Supply TypeName, TypeValue and Specification id</param>
        /// <returns>New SpecificationTypes object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( SpecificationType ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<SpecificationType>> PostSpecificationType(SpecificationType specificationType)
        {
            var res = SpecificationTypeMapper.MapToBll(specificationType);
            _bll.SpecificationTypes.Add(res);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetSpecificationType",
                new
                {
                    id = specificationType.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                specificationType);
        }

        // DELETE: api/SpecificationTypes/5
        /// <summary>
        /// Delete SpecificationTypes by id
        /// </summary>
        /// <param name="id">SpecificationTypes id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( SpecificationType ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecificationType(Guid id)
        {
            var specificationType = await _bll.SpecificationTypes.FirstOrDefaultAsync(id);
            if (specificationType == null)
            {
                return NotFound();
            }

            _bll.SpecificationTypes.Remove(specificationType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecificationTypeExists(Guid id)
        {
            return _bll.SpecificationTypes.Exists(id);
        }
    }
}
