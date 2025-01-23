using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Interfaces.Domain
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
        void SoftDelete();
    }
}
