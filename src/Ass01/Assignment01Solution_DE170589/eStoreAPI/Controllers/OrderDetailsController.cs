using BusinessObject.Entity;
using DataAccess.Repository;
using eStoreAPI.Dtos;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private IOrderDetailRepository repository = new OrdeDetailRepository();

        [HttpGet]
        public ActionResult<IEnumerable<OrderDetail>> GetAlls() => repository.GetAllOrderDetail().ToList();

        //[HttpPost]
        //public IActionResult AddOrder(OrderDetailDTO dto)
        //{
        //    var orderLine = dto.Adapt<OrderDetail>();
        //    repository.AddNewOrderDetail(orderLine);
        //    return NoContent();
        //}
        [HttpPost]
        public IActionResult AddListOrder(List<OrderDetailDTO> dto)
        {
            var orderLine = dto.Adapt<List<OrderDetail>>();
            repository.AddOrderDetails(orderLine);
            return NoContent();
        }
    }
}
