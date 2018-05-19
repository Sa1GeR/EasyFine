using PrivateForum.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PrivateForum.Domain.Extension.Storage
{
    public interface IStorageProvider
    {
        Task<UploadFileResult> UploadFileAsync(string containerName, string fileName, Stream fileStream);
        Task<List<UploadFileInfoResult>> UploadFilesAsync(string containerName, List<FileStreamModel> files);
        Task<Blob> GetFileAsync(string containerName, string fileName);
    }
}
