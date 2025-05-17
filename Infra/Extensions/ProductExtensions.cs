using Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Extensions
{
    public static class ProductExtensions
    {
        public static void SetProductId(this List<ProductEntity> productList, ProductEntity product, ProductEntity? productDuplicateCheck)
        {
            if (productDuplicateCheck is null)
            {
                var id = 1;
                if (productList.Count >= 1)
                {
                    id = productList.Max(x => x.Id) + 1;
                }
                product.Id = id;
                productList.Add(product);
            }
        }

        public static ProductEntity? DuplicateCheck(this List<ProductEntity> productList, ProductEntity product)
        {
            var productDuplicateCheck = productList
                .FirstOrDefault(x => x.Name
                .Equals(product.Name, StringComparison.OrdinalIgnoreCase));

            if (productDuplicateCheck is not null)
            {
                productDuplicateCheck.Quantity += product.Quantity;
            }

            return productDuplicateCheck;
        }
    }
}
