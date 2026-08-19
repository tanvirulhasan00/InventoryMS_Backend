using Asp.Versioning;
using InventoryMS.Models.Entities.ApplicationUserModel.Dto;
using InventoryMS.Models.Entities.CustomerModel;
using InventoryMS.Models.Entities.CustomerModel.Dto;
using InventoryMS.Models.Request;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace InventoryMS.Api.Controllers
{
    [Route("api/v{version:apiVersion}/customer")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CustomerController(IServiceManager service) : ControllerBase
    {
        [HttpGet]
        [Route("get-all")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> GetAllCustomer(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var result = await service.CustomerService.GetAllAsync(new GenericRequest<Customer>
                {
                    Expression = null,
                    CancellationToken = cancellationToken

                });
                if (result == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Customer data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Results = result;
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }

        [HttpGet]
        [Route("get-by-id")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> GetCustomerById(string CustomerId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (CustomerId == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Id";
                    return response;
                }
                var result = await service.CustomerService.GetAsync(new GenericRequest<Customer> { Expression = c => c.CustomerId.ToString() == CustomerId, CancellationToken = cancellationToken });
                if (result == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Customer data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Results = result;
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> CreateCustomer(CreateCustomerReqDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (request == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid request data";
                    return response;
                }
                Customer toCreate = new()
                {
                    CustomerName = request.CustomerName,
                    CompanyName = request.CompanyName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Address = request.Address,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true,
                    IsDeleted = false
                    
                };
                cancellationToken.ThrowIfCancellationRequested();
                await service.CustomerService.AddAsync(toCreate, cancellationToken);
                int result = await service.Save(cancellationToken);
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to create customer";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successfully Created";
                return response;

            }
            catch (OperationCanceledException ex) when (cancellationToken.IsCancellationRequested)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }


        [HttpPost]
        [Route("update")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> UpdateCustomer(UpdateCustomerReqDto request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = await service.CustomerService.UpdateCustomerAsync(request, cancellationToken);
            return response;
        }

        [HttpPost]
        [Route("update-phone-number")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> UpdateCustomerPhoneNumber(UpdateCustomerPhoneNumberReqDto request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = await service.CustomerService.UpdateCustomerPhoneNumberAsync(request, cancellationToken);
            return response;
        }

        //hard delete
        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> DeleteCustomer(string CustomerId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                if (CustomerId == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                var customer = await service.CustomerService.GetAsync(new GenericRequest<Customer>
                {
                    Expression = c => c.CustomerId.ToString() == CustomerId.ToString(),
                    CancellationToken = cancellationToken
                });
                if (customer == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = "Customer Not Found";
                    return response;
                }
                service.CustomerService.Remove(customer);
                int r = await service.Save(cancellationToken);
                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to delete customer";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Customer deleted successfully";
                return response;
            }
            catch(OperationCanceledException ex) when (cancellationToken.IsCancellationRequested)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = "Oparetion Canceled";
                response.Error = ex.Message;
                return response;
            }catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = "An Error Occurs";
                response.Error = ex.Message;
                return response;
            }
           
        }

        //hard delete
        [HttpPost]
        [Route("soft-delete")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> SoftDeleteCustomer(SoftDeleteReq request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = await service.CustomerService.SoftDeleteAsync(request,cancellationToken);
            return response;

        }

    }
}
