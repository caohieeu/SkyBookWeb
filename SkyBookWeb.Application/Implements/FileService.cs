using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SkyBookWeb.Application.Interfaces;

namespace SkyBookWeb.Application.Implements
{
    public class FileService : IFileService
    {
        public void UploadImage(string filePath, IFormFile file)
        {
            
        }
    }
}
