using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SkyBookWeb.Application.Interfaces;

namespace SkyBookWeb.Application.Implements
{
    public class FileService : IFileService
    {
        private string _webRootPath;
        public FileService(string webRootPath)
        {
            _webRootPath = webRootPath;
        }

        public string GetImagePath(string? filePath)
        {
            if(!string.IsNullOrEmpty(filePath))
            {
                return Path.Combine(_webRootPath, filePath);
            }
            return string.Empty;
        }

        public void RemoveImage(string? imageUrl)
        {
            if(!string.IsNullOrEmpty(imageUrl))
            {
                var imageName = imageUrl
                    .Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries)
                    .LastOrDefault();
                var path = Path.Combine(_webRootPath, imageUrl);
                
                try
                {
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    } 
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error when remove image", ex);
                }
            }
        }

        public async Task<string> UploadAsync(IFormFile file, string folder)
        {
            var finalFileName = Guid.NewGuid().ToString() + "-" + file.FileName;
            var finalPath = Path.Combine(_webRootPath, folder);

            if (!Directory.Exists(finalPath))
            {
                Directory.CreateDirectory(finalPath);
            }

            using (var fileStream = new FileStream(Path.Combine(finalPath, finalFileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Path.Combine(@"\", folder, finalFileName);
        }
    }
}
