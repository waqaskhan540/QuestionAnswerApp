using Microsoft.AspNetCore.Http;
using QnA.Application.Interfaces;
using System;
using System.IO;

namespace QnA.FileStorage
{
    public class FileSystemStorageProvider : IFileStorageProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public FileSystemStorageProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string SaveFile(byte[] content, string fileType)
        {
            var filename = $"{DateTime.Now.Ticks.ToString()}.{fileType}";
            var path = Directory.GetCurrentDirectory() + "\\wwwroot\\ProfileImages\\" + filename;
            File.WriteAllBytes(path, content);

            var request = _contextAccessor.HttpContext.Request;
            var remotePath = $"{request.Scheme}://{request.Host.ToString()}/ProfileImages/{filename}";
            return remotePath;

        }
    }
}
