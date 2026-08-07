using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMS.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ValueController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public string TestControllerGet()
        {
            return "TestController is working";
        }
    }
}
