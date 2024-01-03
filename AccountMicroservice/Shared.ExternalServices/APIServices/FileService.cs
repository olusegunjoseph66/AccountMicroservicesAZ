using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shared.ExternalServices.Configurations;
using Shared.ExternalServices.DTOs;
using Shared.ExternalServices.Enums;
using Shared.ExternalServices.Interfaces;
using Shared.Utilities.Helpers;

namespace Shared.ExternalServices.APIServices
{
    public class FileService : IFileService
    {
        private readonly FileServiceSetting _fileSetting;
        private readonly IConfiguration _config;

        public FileService(IOptions<FileServiceSetting> fileSetting, IConfiguration _config)
        {
            _fileSetting = fileSetting.Value;
            this._config = _config;
        }

        public async Task<UploadResponse> FileUpload(IFormFile file, CancellationToken cancellationToken)
        {
            var memory = new MemoryStream();
            await file.CopyToAsync(memory, cancellationToken);
            memory.Position = 0;

            string generatedFileName = $"{DateTime.Now.Ticks}.{file.FileName.Split('.').Last()}";

            //var blobClient = new BlobContainerClient(_fileSetting.ConnectionString, _fileSetting.ContainerName);
            var blobClient = new BlobContainerClient(_config["ConnectionStrings:FileService"], _config["FileServiceSetting:Account:ContainerName"]);
            var blob = blobClient.GetBlobClient(generatedFileName);
            await blob.UploadAsync(memory, cancellationToken);

            UploadResponse fileUrl = new()
            {
                CloudUrl = blob.Uri.AbsoluteUri,
                PublicUrl = blob.Uri.AbsoluteUri
            };
            memory.Close();
            return fileUrl;
        }

        public async Task<(UploadResponse?, bool)> FileUpload(string base64String, string uniqueFileName = null!, CancellationToken cancellationToken = default)
        {
            if (!base64String.IsValidBase64String())
                return new(null, false);

            var extension = base64String.GetExtension();
            var base64FileString = base64String.GetBase64String();
            var byteArray = Convert.FromBase64String(base64FileString);

            var memory = new MemoryStream(byteArray, 0, byteArray.Length);

            string generatedFileName = uniqueFileName == null ? $"{DateTime.UtcNow.Ticks}{extension}" : $"{uniqueFileName}{extension}";

            //var blobClient = new BlobContainerClient(_fileSetting.ConnectionString, _fileSetting.ContainerName);
            var blobClient = new BlobContainerClient(_config["ConnectionStrings:FileService"], _config["FileServiceSetting:Account:ContainerName"]);
            var blob = blobClient.GetBlobClient(generatedFileName);
            await blob.UploadAsync(memory, cancellationToken);

            if(string.IsNullOrWhiteSpace(blob.Uri.AbsoluteUri))
                return new(null, false);

            UploadResponse fileUrl = new()
            {
                CloudUrl = blob.Uri.AbsoluteUri,
                PublicUrl = blob.Uri.AbsoluteUri
            };
            memory.Close();
            return (fileUrl, true);
        }

        public async Task DeleteFile(string imageUrl)
        {
            //var blobClient = new BlobContainerClient(_fileSetting.ConnectionString, _fileSetting.ContainerName);
            var blobClient = new BlobContainerClient(_config["ConnectionStrings:FileService"], _config["FileServiceSetting:Account:ContainerName"]);
            var blob = blobClient.GetBlobClient(imageUrl);
            await blob.DeleteIfExistsAsync();
        }
    }
}
