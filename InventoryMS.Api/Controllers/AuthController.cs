using Asp.Versioning;
using InventoryMS.Models.Entities.ApplicationUserModel;
using InventoryMS.Models.Entities.ApplicationUserModel.Dto;
using InventoryMS.Models.Request;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels;
using Microsoft.AspNetCore.Mvc;


namespace InventoryMS.Api.Controllers
{
    [Route("api/v{version:apiVersion}/auth")] 
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController(IServiceManager service) : ControllerBase
    {
        [HttpGet]
        [Route("get-all-user")]
        public async Task<ApiResponse> GetAllUser()
        {
            var response = new ApiResponse();
            try
            {
                var result = await service.AuthService.GetAllAsync(new GenericRequest<ApplicationUser>
                {
                    Expression = null,
                    
                });
                response.Results = result;
                return response;

            }catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<ApiResponse> Login(LoginRequestDto request)
        {

            var response = await service.AuthService.Login(request);
            return response;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<ApiResponse> Registration(RegistrationReqDto request)
        {
            var response = await service.AuthService.Registration(request);
            return response;
        }

    }
}
