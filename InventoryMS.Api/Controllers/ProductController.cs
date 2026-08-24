using Asp.Versioning;
using InventoryMS.Models.Entities.ProductModels;
using InventoryMS.Models.Entities.ProductModels.Dto;
using InventoryMS.Models.Request;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace InventoryMS.Api.Controllers
{
    [Route("api/v{version:apiVersion}/product")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController(IServiceManager service) : ControllerBase
    {
        [HttpGet]
        [Route("get-all")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> GetAllProduct(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var result = await service.ProductService.GetAllAsync(new GenericRequest<Product>
                {
                    Expression = null,
                    IncludeProperties = "Category,Brand,Unit",
                    CancellationToken = cancellationToken

                });
                if (result == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Product data not found";
                    return response;
                }
                var productToShow = result.Select(p => new
                {
                    p.ProductId,
                    p.ProductCode,
                    p.ProductName,
                    p.Category.CategoryName,
                    p.Brand.BrandName,
                    p.Unit.UnitShortName,
                    p.IsActive,
                    p.CreatedAt,
                    p.UpdatedAt
                }).ToList();
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Results = productToShow;
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
        public async Task<ApiResponse> GetProductById(string ProductId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (ProductId == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Id";
                    return response;
                }
                var result = await service.ProductService.GetAsync(new GenericRequest<Product> { Expression = p => p.ProductId.ToString() == ProductId, IncludeProperties = "Category,Brand,Unit", CancellationToken = cancellationToken });
                if (result == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Product data not found";
                    return response;
                }
                var productToShow = new
                {
                    result.ProductId,
                    result.ProductCode,
                    result.ProductName,
                    result.Category.CategoryName,
                    result.Brand.BrandName,
                    result.Unit.UnitShortName,
                    result.IsActive,
                    CreatedAt = result.CreatedAt.ToLocalTime(),
                    UpdatedAt = result.UpdatedAt.ToLocalTime()
                };
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Results = productToShow;
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
        public async Task<ApiResponse> CreateProduct(CreateProductDto request, CancellationToken cancellationToken)
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
                var existingProduct = await service.ProductService.GetAllAsync(new GenericRequest<Product>
                {
                    Expression = null,
                    CancellationToken = cancellationToken
                });
                var nextNumber = existingProduct.Count + 1;
                Product toCreate = new()
                {
                    ProductCode = $"P-{nextNumber:D3}",
                    ProductName = request.ProductName,
                    CategoryId = Guid.Parse(request.CategoryId),
                    BrandId = Guid.Parse(request.BrandId),
                    UnitId = Guid.Parse(request.UnitId),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    
                };
                await service.ProductService.AddAsync(toCreate, cancellationToken);
                int result = await service.Save(cancellationToken);
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to create product";
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
        public async Task<ApiResponse> UpdateProduct(UpdateProductDto request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = await service.ProductService.UpdateProductAsync(request, cancellationToken);
            return response;
        }

        //hard delete
        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> DeleteProduct(string ProductId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                if (ProductId == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                var product = await service.ProductService.GetAsync(new GenericRequest<Product>
                {
                    Expression = p => p.ProductId.ToString() == ProductId.ToString(),
                    CancellationToken = cancellationToken
                });
                if (product == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = "Product Not Found";
                    return response;
                }
                service.ProductService.Remove(product);
                int r = await service.Save(cancellationToken);
                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to delete product";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Product deleted successfully";
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
