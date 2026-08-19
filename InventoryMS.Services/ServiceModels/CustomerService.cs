using InventoryMS.Database.Data;
using InventoryMS.Models.Entities.CustomerModel;
using InventoryMS.Models.Entities.CustomerModel.Dto;
using InventoryMS.Models.Request;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace InventoryMS.Services.ServiceModels
{
    public class CustomerService(InventoryMSDbContext context) : Services<Customer>(context), ICustomerService
    {
        public async Task<ApiResponse> SoftDeleteAsync(SoftDeleteReq request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (request.CustomerId == null || request.CustomerId == "")
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Customer Id";
                    return response;
                }
                var customer = await context.Customers.FirstOrDefaultAsync(c => c.CustomerId.ToString() == request.CustomerId, cancellationToken);
                if (customer == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Customer not found";
                    return response;
                }
                customer.IsActive = false;
                customer.IsDeleted = true;
                customer.DeletedAt = DateTime.UtcNow;

                int r = await context.SaveChangesAsync(cancellationToken);

                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to soft delete customer";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Customer soft deleted successfully";
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
            catch(Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = "An error occurred while updating the customer";
                response.Error = ex.Message;
                return response;
            }
        }

        public async Task<ApiResponse> UpdateCustomerAsync(UpdateCustomerReqDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (request.CustomerId == null || request.CustomerId == "")
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Customer Id";
                    return response;
                }
                var customer = await context.Customers.FirstOrDefaultAsync(c => c.CustomerId.ToString() == request.CustomerId, cancellationToken);
                if (customer == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Customer not found";
                    return response;
                }
                
                customer.CustomerName = (request.CustomerName == "" || request.CustomerName == null) ? customer.CustomerName : request.CustomerName;
                customer.CompanyName = (request.CompanyName == "" || request.CompanyName == null) ? customer.CompanyName : request.CompanyName;
                customer.Email = (request.Email == "" || request.Email == null) ? customer.Email : request.Email;
                customer.Address = (request.Address == "" || request.Address == null) ? customer.Address : request.Address;
                customer.UpdatedAt = DateTime.UtcNow;

                int r = await context.SaveChangesAsync(cancellationToken);

                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to update customer";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Customer updated successfully";
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
                response.Message = "An error occurred while updating the customer";
                response.Error = ex.Message;
                return response;
            }
        }

        public async Task<ApiResponse> UpdateCustomerPhoneNumberAsync(UpdateCustomerPhoneNumberReqDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (request.CustomerId == null || request.CustomerId == "")
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Customer Id";
                    return response;
                }
                var customer = await context.Customers.FirstOrDefaultAsync(c => c.CustomerId.ToString() == request.CustomerId, cancellationToken);
                if (customer == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Customer not found";
                    return response;
                }

                customer.PhoneNumber = (request.PhoneNumber == "" || request.PhoneNumber == null) ? customer.PhoneNumber : request.PhoneNumber;
                customer.UpdatedAt = DateTime.UtcNow;

                int r = await context.SaveChangesAsync(cancellationToken);

                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to update customer phone number";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Customer phone number updated successfully";
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
                response.Message = "An error occurred while updating the customer";
                response.Error = ex.Message;
                return response;
            }
        }
    }
}
