using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SkyBookWeb.Application.Interfaces
{
    public interface IFileService
    {
        void UploadImage(string filePath, IFormFile file);
    }
}
