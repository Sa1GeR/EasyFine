using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core
{
    public interface IDomainRepository
    {
        void Set(IConnectionFactory connection);
    }
}
