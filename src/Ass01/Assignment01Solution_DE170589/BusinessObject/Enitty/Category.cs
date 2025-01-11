using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Enitty
{
    public partial class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = String.Empty;
        public virtual ICollection<Product> Products { get; set; }
    }
}
