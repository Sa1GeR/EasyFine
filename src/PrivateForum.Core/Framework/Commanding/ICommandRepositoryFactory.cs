using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core.Framework.Commanding
{
    public interface IDomainRepositoryFactory
    {
        TRepository Provide<TRepository>(IConnectionFactory connection)
            where TRepository : IDomainRepository;
    }
}
