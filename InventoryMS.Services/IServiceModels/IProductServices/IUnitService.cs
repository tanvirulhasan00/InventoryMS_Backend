using InventoryMS.Models.Entities.ProductModels;
using InventoryMS.Models.Entities.ProductModels.Dto;
using InventoryMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.IServiceModels.IProductServices
{
    public interface IUnitService : IServices<Unit>
    {
        Task<ApiResponse> UpdateUnitAsync(UpdateUnitDto request, CancellationToken cancellationToken);
    }
}
