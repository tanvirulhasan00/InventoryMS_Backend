using Asp.Versioning;
using InventoryMS.Models.Entities.PurchaseModels;
using InventoryMS.Models.Entities.PurchaseModels.Dto;
using InventoryMS.Models.Request;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace InventoryMS.Api.Controllers
{
    [Route("api/v{version:apiVersion}/purchase-header")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PurchaseHeaderController(IServiceManager service) : ControllerBase
    {
        [HttpGet]
        [Route("get-all")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> GetAllPurchaseHeaders(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var result = await service.PurchaseHeaderService.GetAllAsync(new GenericRequest<PurchaseHeader>
                {
                    Expression = null,
                    CancellationToken = cancellationToken

                });
                if (result == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Purchase header data not found";
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
        public async Task<ApiResponse> GetPurchaseHeaderById(string PurchaseId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (PurchaseId == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Id";
                    return response;
                }
                var result = await service.PurchaseHeaderService.GetAsync(new GenericRequest<PurchaseHeader> { Expression = p => p.PurchaseId.ToString() == PurchaseId, CancellationToken = cancellationToken });
                if (result == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Purchase header data not found";
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
        public async Task<ApiResponse> CreatePurchaseHeader(CreatePurchaseHeaderDto request, CancellationToken cancellationToken)
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
                var allPh = await service.PurchaseHeaderService.GetAllAsync(new GenericRequest<PurchaseHeader>
                {
                    Expression = null,
                    CancellationToken = cancellationToken

                });
                var nextNumber = allPh.Count + 1;
                var purCode = $"{nextNumber:D3}";
                var nextInNumber = allPh.Count + 1;
                var inv = $"INV-{nextNumber:D4}";
                PurchaseHeader toCreate = new()
                {
                    PurchaseNo = int.Parse(purCode),
                    SupplierId = Guid.Parse(request.SupplierId),
                    WarehouseId = Guid.Parse(request.WarehouseId),
                    InvoiceNo = inv,
                    PurchaseDate = request.PurchaseDate,
                    Remarks = request.Remarks,
                    Status = request.Status,
                    CreatedBy = Guid.Parse(request.CreatedBy),
                    CreatedAt = DateTime.UtcNow,
                };

                await service.PurchaseHeaderService.AddAsync(toCreate, cancellationToken);
                int result = await service.Save(cancellationToken);
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to create purchase header";
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
        public async Task<ApiResponse> UpdatePurchaseHeader(UpdatePurchaseHeaderDto request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = await service.PurchaseHeaderService.UpdatePurchaseHeaderAsync(request, cancellationToken);
            return response;
        }

        //hard delete
        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> DeletePurchaseHeader(string PurchaseHeaderId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                if (PurchaseHeaderId == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                var purchase = await service.PurchaseHeaderService.GetAsync(new GenericRequest<PurchaseHeader>
                {
                    Expression = p => p.PurchaseId.ToString() == PurchaseHeaderId.ToString(),
                    CancellationToken = cancellationToken
                });
                if (purchase == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = "Purchase Header Not Found";
                    return response;
                }
                service.PurchaseHeaderService.Remove(purchase);
                int r = await service.Save(cancellationToken);
                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to delete purchase header";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Purchase Header deleted successfully";
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
