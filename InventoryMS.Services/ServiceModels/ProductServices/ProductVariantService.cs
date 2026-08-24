using InventoryMS.Database.Data;
using InventoryMS.Models.Entities.ProductModels;
using InventoryMS.Models.Entities.ProductModels.Dto;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels.IProductServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace InventoryMS.Services.ServiceModels.ProductServices
{
    public class ProductVariantService(InventoryMSDbContext context) : Services<ProductVariant>(context), IProductVariantService
    {
        public async Task<ApiResponse> UpdateProductVariantAsync(UpdateProductVariantDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (request.ProductId == null || request.ProductId == "")
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Product Id";
                    return response;
                }
                var productVariant = await context.ProductVariants.FirstOrDefaultAsync(p => p.VariantId.ToString() == request.VariantId, cancellationToken);
                if (productVariant == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Product variant not found";
                    return response;
                }

                productVariant.ProductId = (request.ProductId == "" || request.ProductId == null) ? productVariant.ProductId : Guid.Parse(request.ProductId);
                productVariant.ColorId = (request.ColorId == "" || request.ColorId == null) ? productVariant.ColorId : Guid.Parse(request.ColorId);
                productVariant.SizeId = (request.SizeId == "" || request.SizeId == null) ? productVariant.SizeId : Guid.Parse(request.SizeId);
                productVariant.LotId = (request.LotId == "" || request.LotId == null) ? productVariant.LotId : Guid.Parse(request.LotId);
                productVariant.SKU = (request.SKU == "" || request.SKU == null) ? productVariant.SKU : request.SKU;
                productVariant.Barcode = (request.Barcode == "" || request.Barcode == null) ? productVariant.Barcode : request.Barcode;
                productVariant.MinimumStock = (request.MinimumStock == 0) ? productVariant.MinimumStock : request.MinimumStock;
                productVariant.UpdatedAt = DateTime.UtcNow;

                int r = await context.SaveChangesAsync(cancellationToken);

                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to update product variant";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Product variant updated successfully";
                return response;
            }
            catch (OperationCanceledException ex) when (cancellationToken.IsCancellationRequested)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = "Request was canceled";
                response.Error = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = "An error occurred while updating the brand";
                response.Error = ex.Message;
                return response;
            }
        }
    }
}
