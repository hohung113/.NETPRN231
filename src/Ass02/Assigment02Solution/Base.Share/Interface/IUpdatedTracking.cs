using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Share.Interface
{
    public interface IUpdatedTracking
    {
        DateTimeOffset UpdatedAt { get; set; }
        Guid UpdatedBy { get; set; }
    }
}
