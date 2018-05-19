using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core.Utilities
{

    [AttributeUsage(AttributeTargets.Field)]
    public class IdentityCodeAttribute : Attribute
    {

        /// <summary>
        /// The Identity Code
        /// </summary>
        public string IdentityCode { get { return identityCode; } }
        private string identityCode;

        public IdentityCodeAttribute(string identityCode)
        {
            this.identityCode = identityCode;
        }

    }
    
}
