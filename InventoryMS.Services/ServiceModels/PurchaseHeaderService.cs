using InventoryMS.Database.Data;
using InventoryMS.Models.Entities.PurchaseModels;
using InventoryMS.Models.Entities.PurchaseModels.Dto;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InventoryMS.Services.ServiceModels
{
    public class PurchaseHeaderService(InventoryMSDbContext context) : Services<PurchaseHeader>(context), IPurchaseHeaderService
    {
        public async Task<ApiResponse> UpdatePurchaseHeaderAsync(UpdatePurchaseHeaderDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                //if(request.PurchaseId == null || request.PurchaseId == "")
                //{
                //    response.Success = false;
                //    response.StatusCode = HttpStatusCode.BadRequest;
                //    response.Message = "PurchaseId is required.";
                //    return response;
                //}
                //var purchaseHeader = await context.PurchaseHeaders.FirstOrDefaultAsync(p => p.PurchaseId.ToString() == request.PurchaseId, cancellationToken);
                //if (purchaseHeader == null)
                //{
                //    response.Success = false;
                //    response.StatusCode = HttpStatusCode.NotFound;
                //    response.Message = "Purchase header not found";
                //    return response;
                //}

                //purchaseHeader.PurchaseNo = request.PurchaseNo;
                //purchaseHeader.SupplierId = Guid.Parse(request.SupplierId);
                //purchaseHeader.WarehouseId = Guid.Parse(request.WarehouseId);
                //purchaseHeader.Remarks = request.Remarks;
                //purchaseHeader.UpdatedAt = DateTime.UtcNow;

                //int r = await context.SaveChangesAsync(cancellationToken);

                //if (r == 0)
                //{
                //    response.Success = false;
                //    response.StatusCode = HttpStatusCode.InternalServerError;
                //    response.Message = "Failed to update purchase header";
                //    return response;
                //}
                //response.Success = true;
                //response.StatusCode = HttpStatusCode.OK;
                //response.Message = "Purchase header updated successfully";
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

        public Task<ApiResponse> UpdatePurchaseHeaderStatusAsync(UpdatePurchaseHeaderStatusDto request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
