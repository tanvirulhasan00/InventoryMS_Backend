using InventoryMS.Models.Entities.PhoneNumberModel;
using InventoryMS.Models.Entities.PhoneNumberModel.Dto;
using InventoryMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.IServiceModels.IProductServices
{
    public interface IPhoneNumberService : IServices<PhoneNumber>
    {
        Task<ApiResponse> UpdatePhoneNumberAsync(UpdatePhoneNumberDto request, CancellationToken cancellationToken);
    }
}
