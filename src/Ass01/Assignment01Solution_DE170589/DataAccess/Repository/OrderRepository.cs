using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void AddOrder(Order od)  => OrderDAO.Instance.AddOrder(od);

        public IEnumerable<Order> GetAllOrders() => OrderDAO.Instance.GetAllOrders();

        public IEnumerable<Order> GetOrderByMemberId(int id) => OrderDAO.Instance.GetOrderByMemberId(id);

        public IEnumerable<Order> GetOrderByOrderDate(DateOnly date,int memberID) => OrderDAO.Instance.GetOrderByOrderDate(date, memberID);
    }
}
