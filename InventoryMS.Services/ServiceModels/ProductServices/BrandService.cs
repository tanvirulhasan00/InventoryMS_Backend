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
    public class BrandService(InventoryMSDbContext context) : Services<Brand>(context), IBrandService
    {
        public async Task<ApiResponse> UpdateBrandAsync(UpdateBrandDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (request.BrandId == null || request.BrandId == "")
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Brand Id";
                    return response;
                }
                var brand = await context.Brands.FirstOrDefaultAsync(c => c.BrandId.ToString() == request.BrandId, cancellationToken);
                if (brand == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Brand not found";
                    return response;
                }

                brand.BrandName = (request.BrandName == "" || request.BrandName == null) ? brand.BrandName : request.BrandName;
                brand.BrandDescription = (request.BrandDescription == "" || request.BrandDescription == null) ? brand.BrandDescription : request.BrandDescription;
                brand.LicenseNo = (request.LicenseNo == "" || request.LicenseNo == null) ? brand.LicenseNo : request.LicenseNo;
                brand.UpdatedAt = DateTime.UtcNow;

                int r = await context.SaveChangesAsync(cancellationToken);

                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to update brand";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Brand updated successfully";
                return response;
            }
            catch(OperationCanceledException ex) when (cancellationToken.IsCancellationRequested)
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
