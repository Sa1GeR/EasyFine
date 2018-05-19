using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core.Framework.Logging
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message, Exception exception);
        void Debug(string message);
    }
}
