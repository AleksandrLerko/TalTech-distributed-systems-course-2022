using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Feedback = App.Public.DTO.v1.Feedback;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FeedbacksController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public FeedbacksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Feedbacks
        /// <summary>
        /// Gets all the feedbacks from the rest backend
        /// </summary>
        /// <returns>All the feedbacks</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<Feedback> ), 200 )] 
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            var res = (await _bll.Feedbacks.GetAllAsync(User.GetUserId()))
                .Select(x => FeedbackMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/Feedbacks/5
        /// <summary>
        /// Get exactly needed you feedback by id from the rest backend
        /// </summary>
        /// <param name="id">Supply feedback id</param>
        /// <returns>Feedback by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Feedback ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback(Guid id)
        {
            var feedback = await _bll.Feedbacks.FirstOrDefaultAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            var res = FeedbackMapper.MapFromBll(feedback);

            return res;
        }

        // PUT: api/Feedbacks/5
        /// <summary>
        /// Insert new data to feedback found by id, edit feedback in the rest backend
        /// </summary>
        /// <param name="id">Supply feedback id</param>
        /// <param name="feedback">Supply feedback value</param>
        /// <returns>Updated feedback object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Feedback ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(Guid id, Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return BadRequest();
            }

            var feedbackDb = await _bll.Feedbacks.FirstOrDefaultAsync(id);
            
            if (feedbackDb == null)
            {
                return NotFound();
            }

            feedbackDb.Value = feedback.Value;
            feedbackDb.TimeWhenPosted = feedback.TimeWhenPosted;
            
            _bll.Feedbacks.Update(feedbackDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
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

        // POST: api/Feedbacks
        /// <summary>
        /// Create new feedback object in the rest backend
        /// </summary>
        /// <param name="feedback">Supply feedback value</param>
        /// <returns>New feedback object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Feedback ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        {
            var feedbackDb = FeedbackMapper.MapToBll(feedback);
            _bll.Feedbacks.Add(feedbackDb);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetFeedback",
                new
                {
                    id = feedback.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                feedback);
        }

        // DELETE: api/Feedbacks/5
        /// <summary>
        /// Delete feedback object by id from the rest backend
        /// </summary>
        /// <param name="id">Supply feedback id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Feedback ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(Guid id)
        {
            var feedback = await _bll.Feedbacks.FirstOrDefaultAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _bll.Feedbacks.Remove(feedback);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool FeedbackExists(Guid id)
        {
            return _bll.Feedbacks.Exists(id);
        }
    }
}
