using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Category = App.Public.DTO.v1.Category;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Gets all categories from the rest backend
        /// </summary>
        /// <returns>All the categories</returns>
        // GET: api/Categories
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<Category> ), 200 )] 
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var res = (await _bll.Categories.GetAllAsync())
                .Select(x => CategoryMapper.MapFromBll(x)).ToList();
            return res;
        }

        /// <summary>
        /// Get exactly needed you category by id from the rest backend
        /// </summary>
        /// <param name="id">Supply category id</param>
        /// <returns>Category by id</returns>
        // GET: api/Categories/5
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Category ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);
            
            if (category == null)
            {
                return NotFound();
            }

            var res = CategoryMapper.MapFromBll(category);
            return res;
        }

        /// <summary>
        /// Insert new data to category found by id, edit category in the rest backend 
        /// </summary>
        /// <param name="id">Supply category id</param>
        /// <param name="category">Supply category name</param>
        /// <returns>Updated category object</returns>
        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Category ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            
            var categoryDb = await _bll.Categories.FirstOrDefaultAsync(id);
            if (categoryDb == null)
            {
                return NotFound();
            }

            categoryDb.CategoryName.SetTranslation(category.CategoryName);
            _bll.Categories.Update(categoryDb);
            await _bll.SaveChangesAsync();
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        /// <summary>
        /// Create new category object in the rest backend
        /// </summary>
        /// <param name="category">Supply category name</param>
        /// <returns>New category object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Category ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory([FromBody] Category category)
        {
            var categoryDb = CategoryMapper.MapToBll(category);
            categoryDb.CategoryName.SetTranslation(category.CategoryName);
            _bll.Categories.Add(categoryDb);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetCategory",
                new
                {
                    id = category.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                category);
        }

        // DELETE: api/Categories/5
        /// <summary>
        /// Delete category object by id form the rest backend
        /// </summary>
        /// <param name="id">Supply category id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Category ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            
            _bll.Categories.Remove(category);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(Guid id)
        {
            return _bll.Categories.Exists(id);
        }
    }
}
