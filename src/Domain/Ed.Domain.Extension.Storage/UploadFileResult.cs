using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Domain.Extension.Storage
{
    public class UploadFileResult
    {
        public bool IsSuccess { get; set; }
        public Uri Url { get; set; }
    }
}
