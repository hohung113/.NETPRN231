using BusinessObject.Enitty;
using BusinessObject.Entity;
using DataAccess.Repository;
using eStoreAPI.Dtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml;


namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();

        [HttpGet]
        //[Authorize(Roles ="User")]
        public ActionResult<IEnumerable<Product>> GetAlls() => repository.GetAll();

        [HttpGet("search")]
        public ActionResult<IEnumerable<Product>> GetPByName([FromQuery] string? text, [FromQuery] int? categoryId)
        {
            return repository.GetProductByName(text, categoryId);
        }
        [HttpGet("range")]
        public ActionResult<IEnumerable<Product>> GetProductByRange(decimal minPrice, decimal maxPrice)
        {
            return repository.GetProductByPriceRange(minPrice,maxPrice).ToList();
        }
        [HttpPost]
        [Authorize]
        public IActionResult CreateProduct([FromBody] ProductDTO p)
        {
            var product = p.Adapt<Product>();
            repository.AddProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var p = repository.GetProductById(id);
            if (p == null)
            {
                return NotFound();
            }
            repository.DeleteProduct(p);
            return NoContent();
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            return repository.GetProductById(id);
        }
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateProduct(int id, ProductDTO dto)
        {
            var p = repository.GetProductById(id);
            if (p == null)
            {
                return NotFound();
            }
            p = dto.Adapt<Product>();
            p.ProductId = id;
            repository.UpdateProduct(p);
            return NoContent();
        }

    }
}
