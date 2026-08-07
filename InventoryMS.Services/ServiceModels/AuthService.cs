using InventoryMS.Models.Entities.ApplicationUserModel.Dto;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.ServiceModels
{
    public class AuthService : IAuthService
    {
        public bool IsUniqueUser(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> Login(LoginRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> Registration()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> ResetPassword()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdatePassword()
        {
            throw new NotImplementedException();
        }
    }
}
