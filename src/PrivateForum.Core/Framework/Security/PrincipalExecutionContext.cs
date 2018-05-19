using System;
using System.Collections.Generic;
using System.Threading;

namespace PrivateForum.Core.Framework.Security
{
    public class PrincipalExecutionContext : IAuditContext
    {
        public PrincipalExecutionContext()
        {

        }
        
        public string ModifiedBy
        {
            get
            {
                return Thread.CurrentPrincipal.Identity.Name;
            }
        }
        
        public virtual DateTime ModificationDate
        {
            get
            {
                return DateTime.Now;
            }
        }

        public string DisplayName
        {
            get
            {
                return Thread.CurrentPrincipal.Identity.Name;
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

        public string FirstName
        {
            get
            {
                return Thread.CurrentPrincipal.Identity.Name;
            }
        }
    }
}
