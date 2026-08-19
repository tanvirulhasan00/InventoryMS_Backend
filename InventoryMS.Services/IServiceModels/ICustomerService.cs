using InventoryMS.Models.Entities.CustomerModel;
using InventoryMS.Models.Entities.CustomerModel.Dto;
using InventoryMS.Models.Request;
using InventoryMS.Models.Response;


namespace InventoryMS.Services.IServiceModels
{
    public interface ICustomerService : IServices<Customer>
    {
        Task<ApiResponse> UpdateCustomerAsync(UpdateCustomerReqDto request, CancellationToken cancellationToken);
        Task<ApiResponse> UpdateCustomerPhoneNumberAsync(UpdateCustomerPhoneNumberReqDto request, CancellationToken cancellationToken);
        Task<ApiResponse> SoftDeleteAsync(SoftDeleteReq request, CancellationToken cancellationToken);

    }
}
