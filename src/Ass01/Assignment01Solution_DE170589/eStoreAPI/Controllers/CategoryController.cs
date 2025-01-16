using BusinessObject.Enitty;
using BusinessObject.Entity;
using DataAccess.Repository;
using eStoreAPI.Dtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
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
            return _catetoryRepository.GetCategories().Select( x => new Category
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
            }).ToList();
        }

        [HttpGet("getcatebyid/{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            return _catetoryRepository.GetCategory(id);
        }

        [HttpPost]
        public ActionResult CreateCategory([FromBody] CategoryDTO cate)
        {
            // Mapster
            var dto = cate.Adapt<Category>();
            _catetoryRepository.AddCategory(dto);
            return NoContent();
        }

        [HttpPost("TestEnum")]
        public ActionResult CreateCategory22(Base tesy)
        {
            // Mapster
            //var dto = cate.Adapt<Category>();
           // _catetoryRepository.AddCategory(dto);
            return NoContent();
        }
    }
    public enum TEST
    {
        grade,
        gradeB
    }
    public class UserTest
    {
        public int Number { get; set; }
    }

    public class Base : UserTest
    {
        public int One { get; set; }
    }
}
