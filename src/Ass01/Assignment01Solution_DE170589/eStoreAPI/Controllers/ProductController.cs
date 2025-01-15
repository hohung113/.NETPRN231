using BusinessObject.Enitty;
using BusinessObject.Entity;
using DataAccess.Repository;
using eStoreAPI.Dtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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

        [HttpGet("name/{text}")]
        public ActionResult<IEnumerable<Product>> GetPByName(string text)
        {
            return repository.GetProductByName(text);
        }
        [HttpGet("range")]
        public ActionResult<IEnumerable<Product>> GetProductByRange(decimal minPrice, decimal maxPrice)
        {
            return repository.GetProductByPriceRange(minPrice,maxPrice).ToList();
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductDTO p)
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

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id)
        {
            var p = repository.GetProductById(id);
            if (p == null)
            {
                return NotFound();
            }
            repository.UpdateProduct(p);
            return NoContent();
        }

    }
}
