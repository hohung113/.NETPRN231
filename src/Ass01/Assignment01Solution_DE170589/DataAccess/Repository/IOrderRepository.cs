using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> GetAllOrders();
        public int AddOrder(Order od);
        public IEnumerable<Order> GetOrderByMemberId(int id);
        public IEnumerable<Order> GetOrderByOrderDate(DateOnly date, int memberID);
    }
}
