using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Domain.Extension.Storage
{
    public class Blob
    {
        public static Blob Empty = new Blob();

        private ICloudBlob _blob;
        internal Blob(ICloudBlob blob)
        {
            _blob = blob;
        }

        private Blob()
        {

        }

        public Task<Stream> OpenReadAsync()
        {
            if (_blob == null)
                return null;

            return _blob.OpenReadAsync();
        }

        public void DownloadToStream(Stream stream)
        {
            if (_blob == null)
                return;

            _blob.DownloadToStream(stream);
        }

        public Task<bool> ExistsAsync()
        {
            if (_blob == null)
                return Task.FromResult(false);

            return _blob.ExistsAsync();
        }
    }
}
