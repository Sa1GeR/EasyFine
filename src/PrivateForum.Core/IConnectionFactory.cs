using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core
{
    public interface IConnectionFactory : IDisposable
    {
        DbConnection Connection { get; }

        DbTransaction Transaction { get; }

        void Begin();
        void Commit();
        void Rollback();
    }
}
