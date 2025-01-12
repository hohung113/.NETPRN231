using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrdeDetailRepository : IOrderDetailRepository
    {
        public void AddNewOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.Instance.AddNewOrderDetail(orderDetail);

        public List<OrderDetail> GetAllOrderDetail() => OrderDetailDAO.Instance.GetOrdersDetails();

        public List<OrderDetail> GetOrdersDetails()
        {
            throw new NotImplementedException();
        }
    }
}
