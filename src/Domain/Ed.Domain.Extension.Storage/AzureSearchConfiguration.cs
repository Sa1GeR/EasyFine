using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Domain.Extension.Storage
{
    public class AzureSearchConfiguration
    {
        public string HelpServiceName { get; set; }
        public string HelpServiceApiKey { get; set; }
        public string HelpServiceIndex { get; set; }
    }
}
