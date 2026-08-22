using InventoryMS.Models.Entities.SupplierModel;
using InventoryMS.Models.Entities.SupplierModel.Dto;
using InventoryMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.IServiceModels.IProductServices
{
    public interface ISupplierService : IServices<Supplier>
    {
        Task<ApiResponse> UpdateSupplierAsync(UpdateSupplierDto request, CancellationToken cancellationToken);
    }
}
