using InventoryMS.Models.Entities.WarehouseModel;
using InventoryMS.Models.Entities.WarehouseModel.Dto;
using InventoryMS.Models.Response;


namespace InventoryMS.Services.IServiceModels
{
    public interface IWarehouseService : IServices<Warehouse>
    {
        Task<ApiResponse> UpdateWarehouseAsync(UpdateWarehouseReqDto request, CancellationToken cancellationToken);
    }
}
