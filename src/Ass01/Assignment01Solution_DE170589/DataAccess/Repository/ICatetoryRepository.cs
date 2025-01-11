using BusinessObject.Enitty;
using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICatetoryRepository
    {
        public void AddCategory(Category c);
        public List<Category> GetCategories();
    }
}
