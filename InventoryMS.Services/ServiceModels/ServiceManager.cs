using InventoryMS.Database.Data;
using InventoryMS.Models.Entities.ApplicationUserModel;
using InventoryMS.Services.IServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }
    }
}
