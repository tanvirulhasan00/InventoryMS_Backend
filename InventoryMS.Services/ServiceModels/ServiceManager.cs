using InventoryMS.Database.Data;
using InventoryMS.Models.Entities.ApplicationUserModel;
using InventoryMS.Services.IServiceModels;
using InventoryMS.Services.IServiceModels.IProductServices;
using InventoryMS.Services.ServiceModels.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;

namespace InventoryMS.Services.ServiceModels
{
    public class ServiceManager(
        InventoryMSDbContext context, 
        UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager, 
        IHttpContextAccessor httpContextAccessor, 
        IConfiguration configuration) : IServiceManager
    {
        private readonly string _secretKey = configuration.GetValue<string>("TokenSetting:SecretKey") ?? "";

        IAuthService IServiceManager.AuthService => new AuthService(context, userManager, roleManager, httpContextAccessor, _secretKey);
        ICustomerService IServiceManager.CustomerService => new CustomerService(context);
        IBrandService IServiceManager.BrandService => new BrandService(context);
        IWarehouseService IServiceManager.WarehouseService => new WarehouseService(context);
        ICategoryService IServiceManager.CategoryService => new CategoryService(context);

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
