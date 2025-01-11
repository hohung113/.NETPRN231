using BusinessObject.Enitty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICatetoryRepository
    {
        CategoryDAO categoryDAO = new CategoryDAO();
        public void AddCategory(Category c) => categoryDAO.AddCategory(c);

        public List<Category> GetCategories() => categoryDAO.GetListCate();
    }
}
