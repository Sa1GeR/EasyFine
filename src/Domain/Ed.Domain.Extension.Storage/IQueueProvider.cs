using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Domain.Extension.Storage
{
    public interface IQueueProvider
    {
        Task<bool> EnqueueAsync(string queueName, dynamic data);
    }
}
