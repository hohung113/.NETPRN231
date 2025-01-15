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
    public class ProductDAO
    {
        public ProductDAO(){}
        public List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using(var _eStoreDb =  new EStoreDbContext())
                {
                    listProducts = _eStoreDb.Products.Include(x => x.Category).ToList();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return listProducts;
        }
        public Product FindProductById(int productId)
        {
            var product = new Product();
            try
            {
                using (var _eStoreDb = new EStoreDbContext())
                {
                    product = _eStoreDb.Products.SingleOrDefault(x => x.ProductId == productId);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return product;
        }
        public void SaveProduct(Product p)
        {
            try
            {
                using (var _eStoreDb = new EStoreDbContext())
                {
                    _eStoreDb.Products.Add(p);
                    _eStoreDb.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public void UpdateProduct(Product p)
        {
            try
            {
                using (var _eStoreDb = new EStoreDbContext())
                {
                    _eStoreDb.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _eStoreDb.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public List<Product> GetProductByName(string name)
        {
            try
            {
                using (var _eStoreDb = new EStoreDbContext())
                {
                    return _eStoreDb.Products.Include(x => x.Category).Where(x => x.ProductName.Contains(name)).ToList();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Product> GetProductByRangePrice(decimal minPrice , decimal maxPrice)
        {
            try
            {
                using (var _eStoreDb = new EStoreDbContext())
                {
                    return _eStoreDb.Products.Where(x => x.UnitPrice >= minPrice && x.UnitPrice <= maxPrice).ToList();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public void DeleteProduct(Product p)
        {
            try
            {
                using (var _eStoreDb = new EStoreDbContext())
                {
                    var p1 = _eStoreDb.Products.SingleOrDefault(x => x.ProductId == p.ProductId);
                    _eStoreDb.Products.Remove(p1);
                    _eStoreDb.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
