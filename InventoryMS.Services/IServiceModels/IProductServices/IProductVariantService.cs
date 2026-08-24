using InventoryMS.Models.Entities.ProductModels;
using InventoryMS.Models.Entities.ProductModels.Dto;
using InventoryMS.Models.Response;

namespace InventoryMS.Services.IServiceModels.IProductServices
{
    public interface IProductVariantService : IServices<ProductVariant>
    {
        Task<ApiResponse> UpdateProductVariantAsync(UpdateProductVariantDto request, CancellationToken cancellationToken);
    }
}
