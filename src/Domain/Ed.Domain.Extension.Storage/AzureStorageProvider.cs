using PrivateForum.Core.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.Domain.Extension.Storage
{
    internal class AzureStorageProvider : IStorageProvider, IQueueProvider
    {
        private CloudStorageAccount storageAccount;

        public AzureStorageProvider(StorageConfiguration storageConfiguration)
        {
            this.storageAccount = CloudStorageAccount.Parse(storageConfiguration.ConnectionString);
        }

        public async Task<bool> EnqueueAsync(string queueName, dynamic data)
        {
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(queueName);

            await queue.CreateIfNotExistsAsync();

            CloudQueueMessage message = new CloudQueueMessage(JsonConvert.SerializeObject(data));
            await queue.AddMessageAsync(message);

            return true;
        }

        public async Task<UploadFileResult> UploadFileAsync(string containerName, string fileName, Stream fileStream)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);

            await blobContainer.CreateIfNotExistsAsync();

            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(RemoveWhitespace(fileName));

            await blob.UploadFromStreamAsync(fileStream);
            
            return new UploadFileResult()
            {
                IsSuccess = true,
                Url = blob.Uri
            };
        }

        public async Task<List<UploadFileInfoResult>> UploadFilesAsync(string containerName, List<FileStreamModel> files)
        {
            var blobClient = storageAccount.CreateCloudBlobClient();

            var blobContainer = blobClient.GetContainerReference(containerName);

            await blobContainer.CreateIfNotExistsAsync();

            var result = new List<UploadFileInfoResult>();

            foreach (var file in files)
            {
                var blob = blobContainer.GetBlockBlobReference(RemoveWhitespace(file.FileName));

                await blob.UploadFromStreamAsync(file.InputStream);

                result.Add(new UploadFileInfoResult
                {
                    IsSuccess = true,
                    Url = blob.Uri,
                    FileName = file.FileName
                });
            }

            return result;
        }

        private static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        public async Task<Blob> GetFileAsync(string containerName, string fileName)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);

            try
            {
                ICloudBlob blob = await blobContainer.GetBlobReferenceFromServerAsync(RemoveWhitespace(fileName));

                return new Blob(blob);
            }
            catch
            {
                return Blob.Empty;
            }
        }

        
    }
}
