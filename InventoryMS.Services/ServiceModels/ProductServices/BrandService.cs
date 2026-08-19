using InventoryMS.Database.Data;
using InventoryMS.Models.Entities.ProductModels;
using InventoryMS.Models.Entities.ProductModels.Dto;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels.IProductServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.ServiceModels.ProductServices
{
    public class BrandService(InventoryMSDbContext context) : Services<Brand>(context), IBrandService
    {
        public Task<ApiResponse> UpdateBrandAsync(UpdateBrandDto request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
