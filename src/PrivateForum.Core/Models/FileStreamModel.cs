using System.IO;

namespace PrivateForum.Core.Models
{
    public class FileStreamModel
    {
        public string FileName { get; set; }

        public Stream InputStream { get; set; }
    }
}
