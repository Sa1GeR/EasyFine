using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core.Framework
{
    /// <summary>
    /// A status code which described the result of the update
    /// </summary>
    public enum UpdateResultStatus
    {
        Success = 1,
        FailedValidation = 2,
        DatabaseError = 3,
        CommunicationError = 4,
        ExecutionFailed = 5,
        ConcurrencyError = 6,
        IntegrationError = 10
    }
}
