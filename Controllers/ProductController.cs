using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Model;
using Product.Process;
using StudentManagementSystem.Controllers;

namespace Product.Controllers
{
    public class ProductController : BaseController
    {
        readonly ProductProcess process;
        [HttpGet] public async Task<IActionResult> Get() => SendResponse(await process.Get(), true);
        [HttpGet("search")]public async Task<IActionResult> Get(string input) => SendResponse(await process.SearchProducts(input), true);
        [HttpPost] public async Task<IActionResult> Post(Products product , List<IFormFile> file) => SendResponse(await process.Create(product, file), true);
        [HttpPut] public async Task<IActionResult> Put(int id, Products product) => SendResponse(await process.Update(id, product), true);
        [HttpDelete] public async Task<IActionResult> Delete(int id) => SendResponse(await process.Delete(id), true);
    }
}
