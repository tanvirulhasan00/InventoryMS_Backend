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
    public class ColorService(InventoryMSDbContext context) : Services<Color>(context), IColorService
    {
        public async Task<ApiResponse> UpdateColorAsync(UpdateColorDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (request.ColorId == null || request.ColorId == "")
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Color Id";
                    return response;
                }
                var color = await context.Colors.FirstOrDefaultAsync(c => c.ColorId.ToString() == request.ColorId, cancellationToken);
                if (color == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Color not found";
                    return response;
                }

                color.ColorName = (request.ColorName == "" || request.ColorName == null) ? color.ColorName : request.ColorName;
                color.ColorCode = (request.ColorCode == "" || request.ColorCode == null) ? color.ColorCode : request.ColorCode;
                color.UpdatedAt = DateTime.UtcNow;

                int r = await context.SaveChangesAsync(cancellationToken);

                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to update color";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Color updated successfully";
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
