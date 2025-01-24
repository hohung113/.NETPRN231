using Base.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Role : BaseEntity
    {
        public string? RoleDescription { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
