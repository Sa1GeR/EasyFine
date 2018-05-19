using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core.Framework.Security
{
    public class ClonedAuditContext : IAuditContext
    {
        public ClonedAuditContext(IAuditContext executionContext)
        {
            ModifiedBy = executionContext.ModifiedBy;
            ModificationDate = executionContext.ModificationDate;
        }
        
        public string ModifiedBy { get; set; }

        public DateTime ModificationDate { get; set; }

        public string DisplayName
        {
            get
            {
                return ModifiedBy;
            }
        }

        public string Id
        {
            get
            {
                return string.Empty;
            }
        }

        public IEnumerable<string> Roles
        {
            get
            {
                return new List<string>();
            }
        }

        public string FirstName => ModifiedBy;
    }
}
