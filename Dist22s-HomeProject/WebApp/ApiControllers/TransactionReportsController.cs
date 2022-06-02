using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionReport = App.Public.DTO.v1.TransactionReport;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TransactionReportsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public TransactionReportsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/TransactionReports
        /// <summary>
        /// Gets all the TransactionReports from the rest backend
        /// </summary>
        /// <returns>All the TransactionReports</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<TransactionReport> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionReport>>> GetTransactionReports()
        {
            var res = (await _bll.TransactionReports.GetAllAsync())
                .Select(x => TransactionReportMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/TransactionReports/5
        /// <summary>
        /// Get exactly needed you TransactionReports by id from the rest backend
        /// </summary>
        /// <param name="id">Supply TransactionReports id</param>
        /// <returns>TransactionReports by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( TransactionReport ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionReport>> GetTransactionReport(Guid id)
        {
            var transactionReport = await _bll.TransactionReports.FirstOrDefaultAsync(id);

            if (transactionReport == null)
            {
                return NotFound();
            }

            var res = TransactionReportMapper.MapFromBll(transactionReport);

            return res;
        }

        // PUT: api/TransactionReports/5
        /// <summary>
        /// Insert new data to TransactionReports found by id, edit TransactionReports in the rest backend
        /// </summary>
        /// <param name="id">Supply TransactionReports id</param>
        /// <param name="transactionReport">Supply CreatedAt time, Order id and/or comment</param>
        /// <returns>Updated TransactionReports object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( TransactionReport ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactionReport(Guid id, TransactionReport transactionReport)
        {
            if (id != transactionReport.Id)
            {
                return BadRequest();
            }

            var transactionReportDb = await _bll.TransactionReports.FirstOrDefaultAsync(id);
            if (transactionReportDb == null)
            {
                return NotFound();
            }
            
            transactionReportDb.Comment!.SetTranslation(transactionReport.Comment!);
            _bll.TransactionReports.Update(transactionReportDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionReportExists(id))
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

        // POST: api/TransactionReports
        /// <summary>
        /// Create new TransactionReports
        /// </summary>
        /// <param name="transactionReport">Supply CreatedAt time, Order id and/or comment</param>
        /// <returns>New TransactionReports object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( TransactionReport ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<TransactionReport>> PostTransactionReport(TransactionReport transactionReport)
        {
            var res = TransactionReportMapper.MapToBll(transactionReport);
            _bll.TransactionReports.Add(res);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetTransactionReport",
                new
                {
                    id = transactionReport.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                transactionReport);
        }

        // DELETE: api/TransactionReports/5
        /// <summary>
        /// Delete TransactionReports by id
        /// </summary>
        /// <param name="id">Supply TransactionReports id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( TransactionReport ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionReport(Guid id)
        {
            var transactionReport = await _bll.TransactionReports.FirstOrDefaultAsync(id);
            if (transactionReport == null)
            {
                return NotFound();
            }

            _bll.TransactionReports.Remove(transactionReport);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionReportExists(Guid id)
        {
            return _bll.TransactionReports.Exists(id);
        }
    }
}
