using App.Contracts.BLL;
using App.Public.DTO.v1;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Currency = App.Public.DTO.v1.Currency;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CurrenciesController(IAppBLL bll)
        {
            _bll = bll;
        }

        
        // GET: api/Currencies
        /// <summary>
        /// Gets all currencies from the rest backend
        /// </summary>
        /// <returns>All the currencies</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<Currency> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies([FromQuery] string? culture)
        {
            Console.WriteLine(culture);
            var res = (await _bll.Currencies.GetAllAsync())
                .Select(x => CurrencyMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/Currencies/5
        /// <summary>
        /// Gets exactly needed you currency by id from the rest backend
        /// </summary>
        /// <param name="id">Supply currency id</param>
        /// <returns>Currency by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Currency ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrency(Guid id)
        {
            var currency = await _bll.Currencies.FirstOrDefaultAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            var res = CurrencyMapper.MapFromBll(currency);
            

            return res;
        }

        // PUT: api/Currencies/5
        /// <summary>
        /// Insert new data to currency found by id, edit currency in the rest backend 
        /// </summary>
        /// <param name="id">Supply currency id</param>
        /// <param name="currency">Supply currency name</param>
        /// <returns>Updated currency object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Currency ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrency(Guid id, Currency currency)
        {
            if (id != currency.Id)
            {
                return BadRequest();
            }

            var currencyDb = await _bll.Currencies.FirstOrDefaultAsync(id);
            if (currencyDb == null)
            {
                return NotFound();
            }
            
            currencyDb.CurrencyName.SetTranslation(currency.CurrencyName);
            _bll.Currencies.Update(currencyDb);
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(id))
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

        // POST: api/Currencies
        /// <summary>
        /// Create new currency object in the rest backend
        /// </summary>
        /// <param name="currency">Supply currency name</param>
        /// <returns>New currency object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Currency ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<Currency>> PostCurrency(Currency currency)
        {
            var currencyDb = CurrencyMapper.MapToBll(currency);
            
            currencyDb.CurrencyName.SetTranslation(currency.CurrencyName);
            _bll.Currencies.Add(currencyDb);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetCurrency", 
                new
                {
                    id = currency.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                currency);
        }

        // DELETE: api/Currencies/5
        /// <summary>
        /// Delete currency object by id from the rest backend 
        /// </summary>
        /// <param name="id">Supply currency id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Currency ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(Guid id)
        {
            var currency = await _bll.Currencies.FirstOrDefaultAsync(id);
            if (currency == null)
            {
                return NotFound();
            }

            _bll.Currencies.Remove(currency);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
        private bool CurrencyExists(Guid id)
        {
            return _bll.Currencies.Exists(id);
        }
    }
}
