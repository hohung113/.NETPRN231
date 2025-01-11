using BusinessObject.Entity;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAlls() => repository.GetAll();

        [HttpGet("name/{text}")]
        public ActionResult<IEnumerable<Product>> GetPByName(string text)
        {
            return repository.GetProductByName(text);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product p)
        {
            repository.AddProduct(p);
            return NoContent();
        }

        [HttpDelete("id")]
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

        [HttpPut("id")]
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
