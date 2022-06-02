using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Public.Mappers;
using Invoice = App.Public.DTO.v1.Invoice;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public InvoicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Invoices
        /// <summary>
        /// Gets all the Invoices from the rest backend
        /// </summary>
        /// <returns>All the Invoices</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<Invoice> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            var res = (await _bll.Invoice.GetAllAsync())
                .Select(x => InvoiceMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/Invoices/5
        /// <summary>
        /// Get exactly needed you Inoivce by id from the rest backend
        /// </summary>
        /// <param name="id">Invoice id</param>
        /// <returns>Invoice by id </returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Invoice ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(Guid id)
        {
            var invoice = await _bll.Invoice.FirstOrDefaultAsync(id);
            
            if (invoice == null)
            {
                return NotFound();
            }

            var res = InvoiceMapper.MapFromBll(invoice);
            return res;
        }

        // PUT: api/Invoices/5
        /// <summary>
        /// Insert new data to Invoice found by id, edit Invoice in the rest backend
        /// </summary>
        /// <param name="id">Supply Invoice id</param>
        /// <param name="invoice">Invoice data to edit (FirstName, LastName, Email, PaymentMethodName, DeliveryMethodName, FullAddress, FinalPrice)</param>
        /// <returns>Updated Invoice object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Invoice ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(Guid id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }
            
            var invoiceDb = await _bll.Invoice.FirstOrDefaultAsync(id);
            if (invoiceDb == null)
            {
                return NotFound();
            }

            invoiceDb = InvoiceMapper.MapToBll(invoice);
            _bll.Invoice.Update(invoiceDb);
            await _bll.SaveChangesAsync();

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoices
        /// <summary>
        /// Create new Invoice object
        /// </summary>
        /// <param name="invoice">Invoice data (FirstName, LastName, Email, PaymentMethodName, DeliveryMethodName, FullAddress, FinalPrice)</param>
        /// <returns>New Invoice object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Invoice ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
            var invoiceDb = InvoiceMapper.MapToBll(invoice);
            _bll.Invoice.Add(invoiceDb);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetInvoice",
                new
                {
                    id = invoice.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                invoice);
        }

        // DELETE: api/Invoices/5
        /// <summary>
        /// Delete Invoice by id from the rest backend
        /// </summary>
        /// <param name="id">Supply Invoice id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var invoice = await _bll.Invoice.FirstOrDefaultAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _bll.Invoice.Remove(invoice);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(Guid id)
        {
            return _bll.Invoice.Exists(id);
        }
    }
}
