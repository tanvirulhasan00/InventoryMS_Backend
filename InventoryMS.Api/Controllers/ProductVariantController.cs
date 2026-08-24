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
    [Route("api/v{version:apiVersion}/product-variant")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductVariantController(IServiceManager service) : ControllerBase
    {
        [HttpGet]
        [Route("get-all")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> GetAllProductVariants(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var result = await service.ProductVariantService.GetAllAsync(new GenericRequest<ProductVariant>
                {
                    Expression = null,
                    IncludeProperties = "Product,Product.Category,Product.Brand,Product.Unit,Color,Size,Lot",
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
                    p.VariantId,
                    p.Product?.ProductName,
                    p.Product?.Category?.CategoryName,
                    p.Product?.Brand?.BrandName,
                    p.Product?.Unit?.UnitShortName,
                    p.Size?.SizeName,
                    p.Color?.ColorName,
                    p.Lot?.LotNumber,
                    p.SKU,
                    p.Barcode,
                    p.MinimumStock,
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
        public async Task<ApiResponse> GetProductVariantById(string ProductVariantId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (ProductVariantId == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Id";
                    return response;
                }
                var result = await service.ProductVariantService.GetAsync(new GenericRequest<ProductVariant> { Expression = pv => pv.VariantId.ToString() == ProductVariantId, IncludeProperties = "Product,Color,Size,Lot", CancellationToken = cancellationToken });
                if (result == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Product variant not found";
                    return response;
                }
                var productToShow = new
                {
                    result.VariantId,
                    result.Product?.ProductName,
                    result.Product?.Category?.CategoryName,
                    result.Product?.Brand?.BrandName,
                    result.Product?.Unit?.UnitShortName,
                    result.Size?.SizeName,
                    result.Color?.ColorName,
                    result.Lot.LotNumber,
                    result.SKU,
                    result.Barcode,
                    result.MinimumStock,
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
        public async Task<ApiResponse> CreateProductVariant(CreateProductVariantDto request, CancellationToken cancellationToken)
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
                ProductVariant toCreate = new()
                {
                    ProductId = Guid.Parse(request.ProductId),
                    ColorId = Guid.Parse(request.ColorId),
                    SizeId = Guid.Parse(request.SizeId),
                    LotId = string.IsNullOrWhiteSpace(request.LotId) ? null : Guid.Parse(request.LotId),
                    SKU = request.SKU,
                    Barcode = request.Barcode,
                    MinimumStock = request.MinimumStock,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    
                };
                await service.ProductVariantService.AddAsync(toCreate, cancellationToken);
                int result = await service.Save(cancellationToken);
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to create product variant";
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
        public async Task<ApiResponse> UpdateProductVariant(UpdateProductVariantDto request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = await service.ProductVariantService.UpdateProductVariantAsync(request, cancellationToken);
            return response;
        }

        //hard delete
        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> DeleteProductVariant(string ProductVariantId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                if (ProductVariantId == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                var productVariant = await service.ProductVariantService.GetAsync(new GenericRequest<ProductVariant>
                {
                    Expression = pv => pv.VariantId.ToString() == ProductVariantId.ToString(),
                    CancellationToken = cancellationToken
                });
                if (productVariant == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = "Product Variant Not Found";
                    return response;
                }
                service.ProductVariantService.Remove(productVariant);
                int r = await service.Save(cancellationToken);
                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to delete product variant";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Product Variant deleted successfully";
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
