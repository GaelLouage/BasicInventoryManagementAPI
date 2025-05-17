using Infra.Dtos;
using Infra.Models;

namespace Infra.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<List<ProductDto>> GetLowStockAsync(Func<ProductEntity, bool> predicate);
        Task<bool> Add(ProductEntity product);
    }
}
