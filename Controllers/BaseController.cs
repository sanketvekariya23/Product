using Microsoft.AspNetCore.Mvc;
using Product.Providers;
using static Product.Model.Common;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
   
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult SendResponse(ApiResponse apiResponse, bool ShowMessage = false)
        {
            if (ShowMessage) { apiResponse.Message ??= Convert.ToString(Enum.Parse<StatusFlag>(Convert.ToString(apiResponse.Status))).AddSpaceBeforeCapital(); }
            return apiResponse.Status == (byte)StatusFlag.Failed ? BadRequest(apiResponse) : Ok(apiResponse);
        }
    }
}