using InventoryMS.Database.Data;
using InventoryMS.Models.Entities.ProductModels;
using InventoryMS.Models.Entities.ProductModels.Dto;
using InventoryMS.Models.Entities.SupplierModel;
using InventoryMS.Models.Entities.SupplierModel.Dto;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels.IProductServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace InventoryMS.Services.ServiceModels.ProductServices
{
    public class SupplierService(InventoryMSDbContext context) : Services<Supplier>(context), ISupplierService
    {
        public async Task<ApiResponse> UpdateSupplierAsync(UpdateSupplierDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (request.SupplierId == null || request.SupplierId == "")
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Supplier Id";
                    return response;
                }
                var supplier = await context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId.ToString() == request.SupplierId, cancellationToken);
                if (supplier == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Supplier not found";
                    return response;
                }

                supplier.SupplierName = (request.SupplierName == "" || request.SupplierName == null) ? supplier.SupplierName : request.SupplierName;
                supplier.SupplierEmail = (request.SupplierEmail == "" || request.SupplierEmail == null) ? supplier.SupplierEmail : request.SupplierEmail;
                supplier.SupplierAddress = (request.SupplierAddress == "" || request.SupplierAddress == null) ? supplier.SupplierAddress : request.SupplierAddress;
                supplier.UpdatedAt = DateTime.UtcNow;

                int r = await context.SaveChangesAsync(cancellationToken);

                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to update supplier";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Supplier updated successfully";
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
