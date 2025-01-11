using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        Product CurrentProduct { get; }
        public List<Product> GetAll();
        public void AddProduct(Product product);
        public void DeleteProduct(Product product);
        public void UpdateProduct(Product product);
        public Product GetProductById(int productID);
        public IEnumerable<Product> GetProductByUnitStock(int unitInStock);
        public IEnumerable<Product> GetProductByPriceRange(decimal minPrice, decimal maxPrice);
        public List<Product> GetProductByName(string productName);

    }
}
