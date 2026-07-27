using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyBookWeb.Application.Dtos
{
    public class FilterDto
    {
        public List<OrderBySelection> OrderBySelections { get; set; } = new List<OrderBySelection>();
    }
    public class OrderBySelection
    {
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
