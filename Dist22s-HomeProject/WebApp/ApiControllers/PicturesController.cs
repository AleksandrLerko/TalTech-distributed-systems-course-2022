using System.Diagnostics;
using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using App.Public.DTO.v1.Identity;
using App.Public.Mappers;
using Picture = App.Public.DTO.v1.Picture;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PicturesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Pictures
        /// <summary>
        /// Gets all the Pictures from the rest backend
        /// </summary>
        /// <returns>All the Pictures</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<Picture> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Picture>>> GetPictures()
        {
            var res = (await _bll.Pictures.GetAllAsync())
                .Select(x => PictureMapper.MapFromBll(x)).ToList();
            return res;
        }

        /// <summary>
        /// Get picture by the product id
        /// </summary>
        /// <param name="productId">Supply product id</param>
        /// <returns>Picture by product id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Picture ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<Picture>> GetPictureByProductId(Guid productId)
        {
            var res = await _bll.Pictures.GetPictureByProductId(productId);
            if (res == null)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "App error",
                    Status = HttpStatusCode.NotFound,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                };

                errorResponse.Errors["id"] = new List<string>()
                {
                    $"Picture with {productId} id not found"
                };
                return NotFound(errorResponse);
            }
            
            return PictureMapper.MapFromBll(res);
        }

        // GET: api/Pictures/5
        /// <summary>
        /// Get exactly needed you Picture by id from the rest backend
        /// </summary>
        /// <param name="id">Supply Picture id</param>
        /// <returns>Picture by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Picture ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<Picture>> GetPicture(Guid id)
        {
            var picture = await _bll.Pictures.FirstOrDefaultAsync(id);

            if (picture == null)
            {
                return NotFound();
            }

            var res = PictureMapper.MapFromBll(picture);
            return res;
        }

        // PUT: api/Pictures/5
        /// <summary>
        /// Insert new data to Picture found by id, edit Picture in the rest backend
        /// </summary>
        /// <param name="id">Supply Picture id</param>
        /// <param name="picture">Supply filepath</param>
        /// <returns>Updated Picture object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Picture ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPicture(Guid id, Picture picture)
        {
            if (id != picture.Id)
            {
                return BadRequest();
            }
            
            var pictureDb = await _bll.Pictures.FirstOrDefaultAsync(id);
            if (pictureDb == null)
            {
                return NotFound();
            }

            pictureDb.FilePath = pictureDb.FilePath;
            _bll.Pictures.Update(pictureDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(id))
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

        // POST: api/Pictures
        /// <summary>
        /// Create new Picture object
        /// </summary>
        /// <param name="picture">Supply filePath </param>
        /// <returns>New Picture object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Picture ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<Picture>> PostPicture(Picture picture)
        {
            var pictureDb = PictureMapper.MapToBll(picture); 
            _bll.Pictures.Add(pictureDb);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetPicture",
                new
                {
                    id = picture.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                picture);
        }

        // DELETE: api/Pictures/5
        /// <summary>
        /// Delete Picture by id from the rest backend
        /// </summary>
        /// <param name="id">Supply Picture id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Picture ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(Guid id)
        {
            var picture = await _bll.Pictures.FirstOrDefaultAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            _bll.Pictures.Remove(picture);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool PictureExists(Guid id)
        {
            return _bll.Pictures.Exists(id);
        }
    }
}
