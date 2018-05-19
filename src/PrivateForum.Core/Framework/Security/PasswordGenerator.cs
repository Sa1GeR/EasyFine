using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core.Framework.Security
{
    public static class PasswordGenerator
    {
        /// <summary>
        /// Generate the password using GUID
        /// </summary>
        /// <returns></returns>
        public static string GeneratePassword() 
        {
            return Guid.NewGuid().ToString();
        }
    }
}
