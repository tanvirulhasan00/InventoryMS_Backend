using Asp.Versioning;
using InventoryMS.Models.Entities.PhoneNumberModel;
using InventoryMS.Models.Entities.ProductModels;
using InventoryMS.Models.Entities.ProductModels.Dto;
using InventoryMS.Models.Entities.SupplierModel;
using InventoryMS.Models.Entities.SupplierModel.Dto;
using InventoryMS.Models.Request;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace InventoryMS.Api.Controllers
{
    [Route("api/v{version:apiVersion}/supplier")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SupplierController(IServiceManager service) : ControllerBase
    {
        [HttpGet]
        [Route("get-all")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> GetAllSupplier(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var result = await service.SupplierService.GetAllAsync(new GenericRequest<Supplier>
                {
                    Expression = null,
                    CancellationToken = cancellationToken

                });
                if (result == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Supplier data not found";
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
        public async Task<ApiResponse> GetSupplierById(string SupplierId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (SupplierId == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Id";
                    return response;
                }
                var result = await service.SupplierService.GetAsync(new GenericRequest<Supplier> { Expression = s => s.SupplierId.ToString() == SupplierId, CancellationToken = cancellationToken });
                if (result == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Supplier data not found";
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
        public async Task<ApiResponse> CreateSupplier(CreateSupplierDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                if (request == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid request data";
                    return response;
                }
                var allSup = await service.SupplierService.GetAllAsync(new GenericRequest<Supplier>
                {
                    Expression = null,
                    CancellationToken = cancellationToken

                });
                var nextNumber = allSup.Count + 1;
                var supCode = $"SUP-{nextNumber:D3}";
                Supplier toCreate = new()
                {
                    SupplierName = request.SupplierName,
                    SupplierCode = supCode,
                    SupplierEmail = request.SupplierEmail,
                    SupplierAddress = request.SupplierAddress,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true,
                    
                };
                await service.SupplierService.AddAsync(toCreate, cancellationToken);
                int result = await service.Save(cancellationToken);
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to create supplier";
                    return response;
                }
                //after creating the supplier, create the phone numbers if any
                if (request.SupplierPhoneNumber.Count > 0)
                {
                    var supplier = await service.SupplierService.GetAsync(new GenericRequest<Supplier>
                    {
                        Expression = s => s.SupplierCode == supCode,
                        CancellationToken = cancellationToken
                    });
                    var supId = supplier.SupplierId;
                    foreach (var phone in request.SupplierPhoneNumber)
                    {
                        PhoneNumber toCreatePhone = new()
                        {
                            OwnerId = supId.ToString(),
                            Number = phone,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow
                        };
                        await service.PhoneNumberService.AddAsync(toCreatePhone, cancellationToken);
                    }
                    int r = await service.Save(cancellationToken);
                    if (r == 0)
                    {
                        response.Success = false;
                        response.StatusCode = HttpStatusCode.InternalServerError;
                        response.Message = "Supplier Created Successfully, but Failed to create supplier phone numbers";
                        return response;
                    }
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
        public async Task<ApiResponse> UpdateSupplier(UpdateSupplierDto request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = await service.SupplierService.UpdateSupplierAsync(request, cancellationToken);
            return response;
        }

        //hard delete
        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> DeleteSupplier(string SupplierId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                if (SupplierId == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                var supplier = await service.SupplierService.GetAsync(new GenericRequest<Supplier>
                {
                    Expression = s => s.SupplierId.ToString() == SupplierId.ToString(),
                    CancellationToken = cancellationToken
                });
                if (supplier == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = "Supplier Not Found";
                    return response;
                }
                service.SupplierService.Remove(supplier);
                int r = await service.Save(cancellationToken);
                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to delete supplier";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Supplier deleted successfully";
                return response;
            }
            catch(OperationCanceledException ex) when (cancellationToken.IsCancellationRequested)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = "Operation Canceled";
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

    }
}
