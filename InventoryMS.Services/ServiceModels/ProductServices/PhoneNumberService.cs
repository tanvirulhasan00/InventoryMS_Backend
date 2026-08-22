using InventoryMS.Database.Data;
using InventoryMS.Models.Entities.PhoneNumberModel;
using InventoryMS.Models.Entities.PhoneNumberModel.Dto;
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
    public class PhoneNumberService(InventoryMSDbContext context) : Services<PhoneNumber>(context), IPhoneNumberService
    {
        public async Task<ApiResponse> UpdatePhoneNumberAsync(UpdatePhoneNumberDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (request.PhoneNumberId == null || request.PhoneNumberId == "")
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Phone Number Id";
                    return response;
                }
                var phoneNumber = await context.PhoneNumbers.FirstOrDefaultAsync(p => p.PhoneNumberId.ToString() == request.PhoneNumberId, cancellationToken);
                if (phoneNumber == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Phone Number not found";
                    return response;
                }

                phoneNumber.Number = (request.Number == "" || request.Number == null) ? phoneNumber.Number : request.Number;
                phoneNumber.UpdatedAt = DateTime.UtcNow;

                int r = await context.SaveChangesAsync(cancellationToken);

                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to update phone number";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Phone Number updated successfully";
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
