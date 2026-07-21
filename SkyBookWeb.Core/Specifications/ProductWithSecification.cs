using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Core.Specifications
{
    public class ProductWithSecification : BaseSpecification<Product>
    {
        public ProductWithSecification([FromQuery] ProductSpecPrams productSpecPrams) : base 
            (x => (string.IsNullOrEmpty(productSpecPrams.Search) || x.Title == productSpecPrams.Search) &&
            (!productSpecPrams.CategoryId.HasValue || x.CategoryId == productSpecPrams.CategoryId)
            )
        {
            AddInclude(x => x.Category);
            AddOrderBy(x => x.Title);
            ApplyPaging(productSpecPrams.PageSize, productSpecPrams.PageSize * productSpecPrams.PageIndex);
            if(!string.IsNullOrEmpty(productSpecPrams.Sort))
            {
                switch(productSpecPrams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Title);
                        break;
                }
            }
        }
    }
}
