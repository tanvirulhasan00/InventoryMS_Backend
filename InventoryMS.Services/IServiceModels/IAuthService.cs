using InventoryMS.Models.Entities.ApplicationUserModel.Dto;
using InventoryMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.IServiceModels
{
    public interface IAuthService
    {
        bool IsUniqueUser(string phoneNumber);
        Task<ApiResponse> Login(LoginRequestDto request);
        Task<ApiResponse> LoginWithCookieAuth(LoginRequestDto request);
        Task<ApiResponse> Registration(RegistrationReqDto request);
        Task<ApiResponse> ResetPassword();
        Task<ApiResponse> UpdatePassword();

    }
}
