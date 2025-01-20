using BusinessObject.Entity;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Enitty;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        EStoreDbContext _context = new EStoreDbContext();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public void AddNewOrderDetail(OrderDetail orderDetail)
        {
            ArgumentNullException.ThrowIfNull(orderDetail, nameof(orderDetail));
            try
            {
                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the orderlines.", ex);
            }
        }

        public List<OrderDetail> GetOrdersDetails()
        {
            return _context.OrderDetails.ToList();
        }

        public List<OrderDetail> GetAllOrderDetail()
        {
            return null;
        }
    }
}
