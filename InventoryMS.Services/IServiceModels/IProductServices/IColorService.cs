using InventoryMS.Models.Entities.ProductModels;
using InventoryMS.Models.Entities.ProductModels.Dto;
using InventoryMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.IServiceModels.IProductServices
{
    public interface IColorService : IServices<Color>
    {
        Task<ApiResponse> UpdateColorAsync(UpdateColorDto request, CancellationToken cancellationToken);
    }
}
