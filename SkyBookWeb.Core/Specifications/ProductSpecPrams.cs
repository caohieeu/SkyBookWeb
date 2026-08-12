using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SkyBookWeb.Core.Specifications
{
    public class ProductSpecPrams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int pageSize { get; set; } = 10;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int? CategoryId { get; set; }
        public int? Id { get; set; }
        public string? Sort { get; set; } = string.Empty;
        private string? search { get; set; }
        public string? Search
        {
            get => search;
            set => search = value?.ToLower() ?? string.Empty;
        }
    }
}
