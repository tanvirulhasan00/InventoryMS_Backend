using InventoryMS.Models.Entities.ProductModels;
using InventoryMS.Models.Entities.ProductModels.Dto;
using InventoryMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.IServiceModels.IProductServices
{
    public interface ICategoryService : IServices<Category>
    {
        Task<ApiResponse> UpdateCategoryAsync(UpdateCategoryDto request, CancellationToken cancellationToken);
    }
}
