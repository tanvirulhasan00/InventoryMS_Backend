using Asp.Versioning;

using InventoryMS.Models.Entities.CustomerModel;
using InventoryMS.Models.Entities.CustomerModel.Dto;
using InventoryMS.Models.Entities.ProductModels;
using InventoryMS.Models.Entities.ProductModels.Dto;
using InventoryMS.Models.Entities.WarehouseModel;
using InventoryMS.Models.Entities.WarehouseModel.Dto;
using InventoryMS.Models.Request;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace InventoryMS.Api.Controllers
{
    [Route("api/v{version:apiVersion}/category")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoryController(IServiceManager service) : ControllerBase
    {
        [HttpGet]
        [Route("get-all")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> GetAllCategory(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var result = await service.CategoryService.GetAllAsync(new GenericRequest<Category>
                {
                    Expression = null,
                    CancellationToken = cancellationToken

                });
                if (result == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Category data not found";
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
        public async Task<ApiResponse> GetCategoryById(string CategoryId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (CategoryId == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Id";
                    return response;
                }
                var result = await service.CategoryService.GetAsync(new GenericRequest<Category> { Expression = c => c.CategoryId.ToString() == CategoryId, CancellationToken = cancellationToken });
                if (result == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Category data not found";
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
        public async Task<ApiResponse> CreateCategory(CreateCategoryDto request, CancellationToken cancellationToken)
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
                Category toCreate = new()
                {
                    CategoryName = request.CategoryName,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true,
                    
                };
                await service.CategoryService.AddAsync(toCreate, cancellationToken);
                int result = await service.Save(cancellationToken);
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to create category";
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
        public async Task<ApiResponse> UpdateCategory(UpdateCategoryDto request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = await service.CategoryService.UpdateCategoryAsync(request, cancellationToken);
            return response;
        }

        //hard delete
        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "admin,manager,housemanager")]
        public async Task<ApiResponse> DeleteCategory(string CategoryId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                if (CategoryId == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                var category = await service.CategoryService.GetAsync(new GenericRequest<Category>
                {
                    Expression = c => c.CategoryId.ToString() == CategoryId.ToString(),
                    CancellationToken = cancellationToken
                });
                if (category == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = "Category Not Found";
                    return response;
                }
                service.CategoryService.Remove(category);
                int r = await service.Save(cancellationToken);
                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to delete category";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Category deleted successfully";
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

    }
}
