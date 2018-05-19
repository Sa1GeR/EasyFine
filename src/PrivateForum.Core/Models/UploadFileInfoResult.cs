using System;

namespace PrivateForum.Core.Models
{
    public class UploadFileInfoResult
    {
        public bool IsSuccess { get; set; }
        public Uri Url { get; set; }
        public string  FileName{ get;set; }
    }
}
