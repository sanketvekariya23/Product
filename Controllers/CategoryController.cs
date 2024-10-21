using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Model;
using Product.Process;
using StudentManagementSystem.Controllers;

namespace Product.Controllers
{
    [Route("api/[controller]")][ApiController]
    public class CategoryController :BaseController
    {
        readonly CategoryProcess process;
        [HttpGet] public async Task<IActionResult> Get() => SendResponse(await process.Get(), true);
        [HttpPost] public async Task<IActionResult> Post(Category category) => SendResponse(await process.Create(category), true);
        [HttpPut] public async Task<IActionResult> Put( int id ,Category category) => SendResponse(await process.Update(id,category), true);
        [HttpDelete] public async Task<IActionResult> Delete(int id) => SendResponse(await process.Delete(id), true);
    }
}
