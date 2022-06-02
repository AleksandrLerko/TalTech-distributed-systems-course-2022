using App.Contracts.BLL;
using App.Public.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CategoryType = App.Public.DTO.v1.CategoryType;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoryTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CategoryTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/CategoryTypes
        /// <summary>
        /// Gets all the categorytypes from the rest backend
        /// </summary>
        /// <returns>All the categorytypes</returns>
        /// 
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<CategoryType> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryType>>> GetCategoryTypes()
        {
            var res = (await _bll.CategoryTypes.GetAllAsync())
                .Select(x => CategoryTypeMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/CategoryTypes/5
        /// <summary>
        /// Get exactly needed you categorytype by id from the rest backend
        /// </summary>
        /// <param name="id">Supply categorytype id</param>
        /// <returns>Categorytype by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( CategoryType ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryType>> GetCategoryType(Guid id)
        {
            var categoryType = await _bll.CategoryTypes.FirstOrDefaultAsync(id);
            
            if (categoryType == null)
            {
                return NotFound();
            }

            return CategoryTypeMapper.MapFromBll(categoryType);
        }

        // PUT: api/CategoryTypes/5
        /// <summary>
        /// Insert new data to categorytype found by id, edit categorytype in the rest backend
        /// </summary>
        /// <param name="id">Supply categorytype id</param>
        /// <param name="categoryType">Supply categorytype name</param>
        /// <returns>Updated categorytype object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( CategoryType ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryType(Guid id, CategoryType categoryType)
        {
            if (id != categoryType.Id)
            {
                return BadRequest();
            }

            var categoryTypeDb = await _bll.CategoryTypes.FirstOrDefaultAsync(id);
            if (categoryTypeDb == null)
            {
                return NotFound();
            }

            categoryTypeDb.TypeName.SetTranslation(categoryType.TypeName);
            _bll.CategoryTypes.Update(categoryTypeDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryTypeExists(id))
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

        // POST: api/CategoryTypes
        /// <summary>
        /// Create new categorytype object in the rest backend
        /// </summary>
        /// <param name="categoryType">Supply categorytype name</param>
        /// <returns>New categorytype object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( CategoryType ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<CategoryType>> PostCategoryType(CategoryType categoryType)
        {
            var categoryTypeDb = CategoryTypeMapper.MapToBll(categoryType);
            categoryTypeDb.TypeName.SetTranslation(categoryType.TypeName);
            _bll.CategoryTypes.Add(categoryTypeDb);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetCategoryType",
                new
                {
                    id = categoryType.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                categoryType);
        }

        // DELETE: api/CategoryTypes/5
        /// <summary>
        /// Delete categorytype object by id from the rest backend
        /// </summary>
        /// <param name="id">Supply categorytype id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( CategoryType ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryType(Guid id)
        {
            var categoryType = await _bll.CategoryTypes.FirstOrDefaultAsync(id);
            if (categoryType == null)
            {
                return NotFound();
            }
            
            _bll.CategoryTypes.Remove(categoryType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryTypeExists(Guid id)
        {
            return _bll.CategoryTypes.Exists(id);
        }
    }
}
