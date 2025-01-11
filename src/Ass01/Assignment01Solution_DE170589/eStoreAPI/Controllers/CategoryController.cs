using BusinessObject.Enitty;
using BusinessObject.Entity;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICatetoryRepository _catetoryRepository = new CategoryRepository();
        [HttpGet]
        public ActionResult<List<Category>> GetCategories()
        {
            return _catetoryRepository.GetCategories();
        }
        [HttpPost]
        public ActionResult CreateCategory([FromBody] Category cate)
        {
            _catetoryRepository.AddCategory(cate);
            return NoContent();
        }
    }
}
