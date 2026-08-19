using InventoryMS.Database.Data;
using InventoryMS.Models.Entities.WarehouseModel;
using InventoryMS.Models.Entities.WarehouseModel.Dto;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace InventoryMS.Services.ServiceModels
{
    public class WarehouseService(InventoryMSDbContext context) : Services<Warehouse>(context), IWarehouseService
    {
        public async Task<ApiResponse> UpdateWarehouseAsync(UpdateWarehouseReqDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if(request.WarehouseId == null || request.WarehouseId == "")
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "WarehouseId is required.";
                    return response;
                }
                var warehouse = await context.Warehouses.FirstOrDefaultAsync(w => w.WarehouseId.ToString() == request.WarehouseId, cancellationToken);
                if (warehouse == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Warehouse not found";
                    return response;
                }

                warehouse.WarehouseName = (request.WarehouseName == null || request.WarehouseName == "") ? warehouse.WarehouseName : request.WarehouseName;
                warehouse.PhoneNumber = (request.PhoneNumber == null || request.PhoneNumber == "") ? warehouse.PhoneNumber : request.PhoneNumber;
                warehouse.Location = (request.Location == null || request.Location == "") ? warehouse.Location : request.Location;
                warehouse.UpdatedAt = DateTime.UtcNow;

                int r = await context.SaveChangesAsync(cancellationToken);

                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to update warehouse";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Warehouse updated successfully";
                return response;


            }
            catch(OperationCanceledException ex) when (cancellationToken.IsCancellationRequested)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = "The operation was canceled."; 
                response.Error = ex;
                return response;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = "An error occurred while updating the warehouse.";
                response.Error = ex;
                return response;
            }
        }
    }
}
