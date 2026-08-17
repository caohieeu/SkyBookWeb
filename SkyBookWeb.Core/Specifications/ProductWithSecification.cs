using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Core.Specifications
{
    public class ProductWithSecification : BaseSpecification<Product>
    {
        public ProductWithSecification([FromQuery] ProductSpecPrams productSpecPrams) : base 
            (x => (string.IsNullOrEmpty(productSpecPrams.Search) || 
            x.Title.Contains(productSpecPrams.Search.Trim())) &&
            (!productSpecPrams.CategoryId.HasValue || x.CategoryId == productSpecPrams.CategoryId) &&
            (!productSpecPrams.Id.HasValue || x.Id == productSpecPrams.Id)
            )
        {
            
            if(productSpecPrams.IncludeCategory)
            {
                AddInclude(x => x.Category);
            }

            AddOrderBy(x => x.Title);
            ApplyPaging(productSpecPrams.PageSize, productSpecPrams.PageSize * (productSpecPrams.PageIndex - 1));
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
                    case "authorAsc":
                        AddOrderBy(x => x.Author);
                        break;
                    case "authorDesc":
                        AddOrderByDescending(x => x.Author);
                        break;
                    case "isbnAsc":
                        AddOrderBy(x => x.ISBN);
                        break;
                    case "isbnDesc":
                        AddOrderByDescending(x => x.ISBN);
                        break;
                    default:
                        AddOrderBy(x => x.Title);
                        break;
                }
            }
        }
    }
}
