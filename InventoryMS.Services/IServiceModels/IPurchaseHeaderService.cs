using InventoryMS.Models.Entities.PurchaseModels;
using InventoryMS.Models.Entities.PurchaseModels.Dto;
using InventoryMS.Models.Response;


namespace InventoryMS.Services.IServiceModels
{
    public interface IPurchaseHeaderService : IServices<PurchaseHeader>
    {
        Task<ApiResponse> UpdatePurchaseHeaderAsync(UpdatePurchaseHeaderDto request, CancellationToken cancellationToken);
        Task<ApiResponse> UpdatePurchaseHeaderStatusAsync(UpdatePurchaseHeaderStatusDto request, CancellationToken cancellationToken);
    }
}
