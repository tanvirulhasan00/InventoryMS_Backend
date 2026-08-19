using InventoryMS.Database.Data;
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
    public class CategoryService(InventoryMSDbContext context) : Services<Category>(context), ICategoryService
    {
        public async Task<ApiResponse> UpdateCategoryAsync(UpdateCategoryDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (request.CategoryId == null || request.CategoryId == "")
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Category Id";
                    return response;
                }
                var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryId.ToString() == request.CategoryId, cancellationToken);
                if (category == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Category not found";
                    return response;
                }

                category.CategoryName = (request.CategoryName == "" || request.CategoryName == null) ? category.CategoryName : request.CategoryName;
                category.Description = (request.Description == "" || request.Description == null) ? category.Description : request.Description;
                category.UpdatedAt = DateTime.UtcNow;

                int r = await context.SaveChangesAsync(cancellationToken);

                if (r == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Failed to update category";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Category updated successfully";
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
