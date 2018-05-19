using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core.Framework.Security
{
    public interface IAuditContext
    {
        string Id { get; }

        IEnumerable<string> Roles { get; }

        string DisplayName { get; }

        string FirstName { get; }

        /// <summary>
        /// Gets the name of the User/Systgem/Business Process performing the modification
        /// </summary>
        string ModifiedBy { get; }

        /// <summary>
        /// Gets the modification date.
        /// </summary>
        DateTime ModificationDate { get; }
    }
}
