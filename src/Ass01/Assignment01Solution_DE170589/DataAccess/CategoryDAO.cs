using BusinessObject.Enitty;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CategoryDAO
    {
        public CategoryDAO(){}
        public List<Category> GetListCate()
        {
            var listCates = new List<Category>();
            try
            {
                using (var _eStoreDb = new EStoreDbContext())
                {
                    listCates = _eStoreDb.Categories.Include(x => x.Products).ToList();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return listCates;
        }
        public Category GetCategory(int id)
        {
            Category category;
            try
            {
                using (var _eStoreDb = new EStoreDbContext())
                {
                    category = _eStoreDb.Categories.Include(x => x.Products).FirstOrDefault(it => it.CategoryId == id);
                   
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return category;
        }
        public void AddCategory(Category category)
        {
            try
            {
                using (var _eStoreDb = new EStoreDbContext())
                {
                    _eStoreDb.Add(category);
                    _eStoreDb.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
