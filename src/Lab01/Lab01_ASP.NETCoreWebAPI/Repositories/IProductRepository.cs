using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IProductRepository
    {
        void SaveProduct(Product p);
        Product GetProductById (int id);
        void DeleteProductById(Product p);
        void UpdateProduct(Product p);
        List<Category> GetAllCategories();
        List<Product> GetProducts();
    }
}
