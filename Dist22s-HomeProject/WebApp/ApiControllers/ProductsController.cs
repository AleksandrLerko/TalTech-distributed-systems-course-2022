using App.Contracts.BLL;
using App.Public.Mappers;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product = App.Public.DTO.v1.Product;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ProductsController(IAppBLL bll, ILogger<ProductsController> logger)
        {
            _bll = bll;
        }

        // GET: api/Products
        /// <summary>
        /// Gets all the products from the rest backend
        /// </summary>
        /// <returns>All the products</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<Product> ), 200 )] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var res = (await _bll.Products.GetAllAsync())
                .Select(x => ProductMapper.MapFromBll(x)).ToList();
            return res;
        }

        // GET: api/Products/5
        /// <summary>
        /// Get exactly needed you product by id from the rest backend
        /// </summary>
        /// <param name="id">Supply product id</param>
        /// <returns>Product by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Product ), StatusCodes.Status200OK )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _bll.Products.FirstOrDefaultAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var res = ProductMapper.MapFromBll(product);
            
            return res;
        }

        /// <summary>
        /// Get Products by category id
        /// </summary>
        /// <param name="categoryId">Supply category id</param>
        /// <returns>List of products by category id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<Product> ), 200 )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(Guid categoryId)
        {
            var res = (await _bll.Products.GetProductsByCategory(categoryId))
                .Select(x => ProductMapper.MapFromBll(x)).ToList();
            return res;
        }
        
        /// <summary>
        /// Get Products by product name
        /// </summary>
        /// <param name="productName">Product name</param>
        /// <returns>List of products by product name</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<Product> ), 200 )] 
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpGet("name/{productName}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string productName)
        {
            var res = (await _bll.Products.GetProductByName(productName))
                .Select(x => ProductMapper.MapFromBll(x)).ToList();
            return res;
        }

        // PUT: api/Products/5
        /// <summary>
        /// Insert new data to product found by id, edit product in the rest backend
        /// </summary>
        /// <param name="id">Supply product id</param>
        /// <param name="product">Supply name, description, price, Category id, Seller id and Currency id</param>
        /// <returns>Updated product object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Product ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [ProducesResponseType( StatusCodes.Status400BadRequest )] 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var productDb = await _bll.Products.FirstOrDefaultAsync(id);
            if (productDb == null)
            {
                return NotFound();
            }

            productDb.Price = product.Price;
            
            productDb.ProductName.SetTranslation(product.ProductName);
            productDb.Description.SetTranslation(product.Description);
            _bll.Products.Update(productDb);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        /// <summary>
        /// Create new product object
        /// </summary>
        /// <param name="product">SSupply name, description, price, Category id, Seller id and Currency id</param>
        /// <returns>New product object</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Product ), StatusCodes.Status201Created )]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            var productDb = ProductMapper.MapToBll(product);
            productDb.ProductName.SetTranslation(product.ProductName);
            productDb.Description.SetTranslation(product.Description);
            _bll.Products.Add(productDb);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetProduct",
                new
                {
                    id = product.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                product);
        }

        // DELETE: api/Products/5
        /// <summary>
        /// Delete product by id from the rest backend
        /// </summary>
        /// <param name="id">Supply product id</param>
        /// <returns></returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( Product ), StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _bll.Products.FirstOrDefaultAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _bll.Products.Remove(product);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _bll.Products.Exists(id);
        }
    }
}
