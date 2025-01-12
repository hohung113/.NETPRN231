using BusinessObject;
using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        ProductDAO productDAO = new ProductDAO();
        public Product CurrentProduct => throw new NotImplementedException();

        public void AddProduct(Product product)
        {
            productDAO.SaveProduct(product);
        }

        public void DeleteProduct(Product product)
        {
            productDAO.DeleteProduct(product);
        }

        public List<Product> GetAll()
        {
            return productDAO.GetProducts();
        }

        public Product GetProductById(int productID)
        {
            return productDAO.FindProductById(productID);
        }

        public List<Product> GetProductByName(string productName)
        {
            return productDAO.GetProductByName(productName);     
        }
        public void UpdateProduct(Product product)
        {
            productDAO.UpdateProduct(product);
        }
        public IEnumerable<Product> GetProductByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return productDAO.GetProductByRangePrice(minPrice, maxPrice);
        }

        public IEnumerable<Product> GetProductByUnitStock(int unitInStock)
        {
            throw new NotImplementedException();
        }
    }
}
