using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        public void AddNewOrderDetail(OrderDetail orderDetail);
        public List<OrderDetail> GetOrdersDetails();
        public List<OrderDetail> GetAllOrderDetail();
    }
}
