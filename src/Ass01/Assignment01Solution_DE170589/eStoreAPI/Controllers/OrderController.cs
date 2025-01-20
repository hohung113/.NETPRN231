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
    public class OrderController : ControllerBase
    {
        private IOrderRepository repository = new OrderRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAlls() => repository.GetAllOrders().ToList();

        [HttpGet("{memberID}")]
        public ActionResult<IEnumerable<Order>> GetOrderMemberId(int memberID)
        {
            return repository.GetOrderByMemberId(memberID).ToList();
        }
        [HttpGet("DateByMember")]
        public ActionResult<IEnumerable<Order>> GetOrderMemberByDate(string date, int memberID)
        {
            if (!DateOnly.TryParse(date, out var parsedDate))
            {
                return BadRequest("Invalid date format. Please use 'yyyy-MM-dd'.");
            }
            var orders = repository.GetOrderByOrderDate(parsedDate, memberID);
            if (!orders.Any())
            {
                return NotFound("No orders found for the given date and member ID.");
            }

            return Ok(orders);
        }
        [HttpPost]
        public IActionResult AddOrder(OrderDTO orderDTO)
        {
            var order = orderDTO.Adapt<Order>();
            int orderId = repository.AddOrder(order);
            return Ok(new { id = orderId });
        }
    }
}
