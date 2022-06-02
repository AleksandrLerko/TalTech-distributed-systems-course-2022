using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentType = App.Public.DTO.v1.PaymentType;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PaymentTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PaymentTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PaymentTypes
        /// <summary>
        /// Gets all the paymenttypes from the rest backend
        /// </summary>
        /// <returns>All the PaymentTypes</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<PaymentType> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentType>>> GetPaymentTypes()
        {
            var res = (await _bll.PaymentTypes.GetAllAsync())
                .Select(x => PaymentTypeMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/PaymentTypes/5
        /// <summary>
        /// Get exactly needed you PaymentTypes by id from the rest backend
        /// </summary>
        /// <param name="id">Supply paymenttype id</param>
        /// <returns>PaymentType by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( PaymentType ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentType>> GetPaymentType(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);

            if (paymentType == null)
            {
                return NotFound();
            }

            var res = PaymentTypeMapper.MapFromBll(paymentType);

            return res;
        }

        // PUT: api/PaymentTypes/5
        /// <summary>
        /// Insert new data to paymenttype found by id, edit paymenttype in the rest backend
        /// </summary>
        /// <param name="id">Supply paymenttype id</param>
        /// <param name="paymentType">Supply typename and/or comment</param>
        /// <returns>Updated paymenttype object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( PaymentType ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentType(Guid id, PaymentType paymentType)
        {
            if (id != paymentType.Id)
            {
                return BadRequest();
            }

            var paymentTypeDb = await _bll.PaymentTypes.FirstOrDefaultAsync(id);
            if (paymentTypeDb == null)
            {
                return NotFound();
            }

            paymentTypeDb.Comment = paymentType.Comment;
            paymentTypeDb.TypeName.SetTranslation(paymentType.TypeName);
            _bll.PaymentTypes.Update(paymentTypeDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTypeExists(id))
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

        // POST: api/PaymentTypes
        /// <summary>
        /// Create new PaymentType object
        /// </summary>
        /// <param name="paymentType">Supply typename and/or comment</param>
        /// <returns>New paymenttype object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( PaymentType ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<PaymentType>> PostPaymentType(PaymentType paymentType)
        {
            var paymentTypeDb = PaymentTypeMapper.MapToBll(paymentType);   
            _bll.PaymentTypes.Add(paymentTypeDb);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetPaymentType",
                new
                {
                    id = paymentType.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                paymentType);
        }

        // DELETE: api/PaymentTypes/5
        /// <summary>
        /// Delete paymenttype by id from the rest backend
        /// </summary>
        /// <param name="id">Supply paymenttype id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( PaymentType ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentType(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            _bll.PaymentTypes.Remove(paymentType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentTypeExists(Guid id)
        {
            return _bll.PaymentTypes.Exists(id);
        }
    }
}
