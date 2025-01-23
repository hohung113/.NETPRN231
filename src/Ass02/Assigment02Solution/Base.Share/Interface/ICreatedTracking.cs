using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Share.Interface
{
    public interface ICreatedTracking
    {
        DateTimeOffset CreatedAt { get; set; }
        Guid CreatedBy { get; set; }
    }
}
