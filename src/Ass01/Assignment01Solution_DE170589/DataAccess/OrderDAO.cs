using BusinessObject;
using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        EStoreDbContext _context = new EStoreDbContext();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Order> GetOrderByMemberId(int id)
        {
            var listOrders = _context.Orders.Include(x => x.OrderDetails).ThenInclude(od => od.Product).Where(it => it.OrderDetails.Any()).ToList();
            return listOrders.Any() ? listOrders.Where(x => x.MemberId == id) : Enumerable.Empty<Order>();
        }
        public IEnumerable<Order> GetOrderByOrderDate(DateOnly date, int memberID)
        {
            var listOrders = _context.Orders
                .Where(x => x.OrderDate.Date == date.ToDateTime(TimeOnly.MinValue).Date && x.MemberId == memberID)
                .Include(x => x.OrderDetails)
                .ToList();

            return listOrders.Any() ? listOrders : Enumerable.Empty<Order>();
        }
        public IEnumerable<Order> GetAllOrders()
        {
            var orders = _context.Orders.Include(x => x.OrderDetails).ToList();
            return orders.Any() ? orders : Enumerable.Empty<Order>();
        }
        public int AddOrder(Order od)
        {
            ArgumentNullException.ThrowIfNull(od, nameof(od));

            try
            {
                _context.Orders.Add(od);
                _context.SaveChanges();
                return od.OrderId; 
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the order.", ex);
            }
        }

    }
}
