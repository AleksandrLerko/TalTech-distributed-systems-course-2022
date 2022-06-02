using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Specification = App.Public.DTO.v1.Specification;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SpecificationsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public SpecificationsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Specifications
        /// <summary>
        /// Gets all the Specifications from the rest backend
        /// </summary>
        /// <returns>All the Specifications</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<Specification> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specification>>> GetSpecifications()
        {
            var res = (await _bll.Specifications.GetAllAsync())
                .Select(x => SpecificationMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/Specifications/5
        /// <summary>
        /// Get exactly needed you Specifications by id from the rest backend
        /// </summary>
        /// <param name="id">Supply Specifications id</param>
        /// <returns>Specifications by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Specification ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<Specification>> GetSpecification(Guid id)
        {
            var specification = await _bll.Specifications.FirstOrDefaultAsync(id);

            if (specification == null)
            {
                return NotFound();
            }

            var res = SpecificationMapper.MapFromBll(specification);
            return res;
        }

        // PUT: api/Specifications/5
        /// <summary>
        /// Insert new data to Specifications found by id, edit Specifications in the rest backend
        /// </summary>
        /// <param name="id">Supply Specifications id</param>
        /// <param name="specification">Supply Specifications name and Product id</param>
        /// <returns>Updated Specification object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Specification ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecification(Guid id, Specification specification)
        {
            if (id != specification.Id)
            {
                return BadRequest();
            }

            var specificationDb = await _bll.Specifications.FirstOrDefaultAsync(id);
            if (specificationDb == null)
            {
                return NotFound();
            }
            specificationDb.SpecificationName.SetTranslation(specification.SpecificationName);
            _bll.Specifications.Update(specificationDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecificationExists(id))
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

        // POST: api/Specifications
        /// <summary>
        /// Create new Specification object
        /// </summary>
        /// <param name="specification">Supply Specifications name and Product id</param>
        /// <returns>New Specification object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Specification ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<Specification>> PostSpecification(Specification specification)
        {
            var res = SpecificationMapper.MapToBll(specification);
            _bll.Specifications.Add(res);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetSpecification",
                new
                {
                    id = specification.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                specification);
        }

        // DELETE: api/Specifications/5
        /// <summary>
        /// Delete Specification by id
        /// </summary>
        /// <param name="id">Supply Specification id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Specification ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecification(Guid id)
        {
            var specification = await _bll.Specifications.FirstOrDefaultAsync(id);
            if (specification == null)
            {
                return NotFound();
            }

            _bll.Specifications.Remove(specification);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecificationExists(Guid id)
        {
            return _bll.Specifications.Exists(id);
        }
    }
}
