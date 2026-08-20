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
    public class ProductService(InventoryMSDbContext context) : Services<Product>(context), IProductService
    {
        public async Task<ApiResponse> UpdateProductAsync(UpdateProductDto request, CancellationToken cancellationToken)
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
                var product = await context.Products.FirstOrDefaultAsync(p => p.ProductId.ToString() == request.ProductId, cancellationToken);
                if (product == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Product not found";
                    return response;
                }

                product.ProductName = (request.ProductName == "" || request.ProductName == null) ? product.ProductName : request.ProductName;
                product.BrandId = (request.BrandId == "" || request.BrandId == null) ? product.BrandId : Guid.Parse(request.BrandId);
                product.UnitId = (request.UnitId == "" || request.UnitId == null) ? product.UnitId : Guid.Parse(request.UnitId);
                product.UpdatedAt = DateTime.UtcNow;

                int r = await context.SaveChangesAsync(cancellationToken);

                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to update product";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Product updated successfully";
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
