using Infra.Dtos;
using Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mappers
{
    public static class ProductDtoMapper
    {
        public static ProductDto MapToProductDto(this ProductEntity product)
        {
            return new ProductDto
            {
                Name = product.Name,
                Quantity = product.Quantity,
            };
        }
    }
}
