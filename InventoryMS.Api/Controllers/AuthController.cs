using Asp.Versioning;
using InventoryMS.Models.Entities.ApplicationUserModel.Dto;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMS.Api.Controllers
{
    [Route("api/v{version:apiVersion}/auth")] 
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController(IServiceManager service) : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ApiResponse> Login(LoginRequestDto request)
        {

            var response = await service.AuthService.Login(request);
            return response;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Test()
        {
            return Ok("test ok");
        }

    }
}
