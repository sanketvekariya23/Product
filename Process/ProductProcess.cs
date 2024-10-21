using Microsoft.EntityFrameworkCore;
using Product.Data;
using Product.Model;
using static Product.Model.Common;

namespace Product.Process
{
    public class ProductProcess
    {
        private const string ImageFolderPath = "imagesProduct";
        public async Task<ApiResponse> Get()
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                var product = await defaultContext.Products.Include(i => i.Images).Select(s => new
                {
                    ProductId = s.ProductId,
                    ProductName = s.ProductName,
                    ProductCode = s.ProductCode.ToString("D6"),    
                    Quantity = s.Quantity,
                    ProductImage = s.Images.Select(i=>i.Url).ToList(),
                    Color = s.Color,
                }).ToListAsync();
                apiResponse.data = product;
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in getting Student data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Create(Products product,List<IFormFile> files)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), ImageFolderPath);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                foreach (var file in files)
                {
                    string filePath = Path.Combine(folderPath, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var imageUrl = new ImageURL
                    {
                        Url = filePath,
                        Products = product
                    };
                    product.Images.Add(imageUrl);
                }

                await defaultContext.Products.AddAsync(product);
                await defaultContext.SaveChangesAsync();
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in Creating products data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Update(int id, Products product)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                var productexist = await defaultContext.Products.FindAsync(id);
                if (productexist != null)
                {
                    defaultContext.Products.Update(product);
                    await defaultContext.SaveChangesAsync();
                }
                else
                {
                    apiResponse.Message = "products not found";
                    apiResponse.Status = (Byte)StatusFlag.Failed;
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in Updating products data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Delete(int productid)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                Products? productexist = defaultContext.Products.AsNoTracking().FirstOrDefault(t => t.ProductId == productid);
                if (productexist != null)
                {
                    defaultContext.Products.Remove(productexist);
                    await defaultContext.SaveChangesAsync();
                }
                else
                {
                    apiResponse.Status = (Byte)StatusFlag.Failed;
                    apiResponse.Message = "Product not found";
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in deleting Product data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> SearchProducts(string keyword)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                var products = await defaultContext.Products
                    .Where(p => p.ProductName.Contains(keyword) || p.Color.Contains(keyword))
                    .Select(p => new
                    {
                        p.ProductId,
                        p.ProductName,
                        p.ProductCode,
                        p.Color,
                        p.Quantity
                    })
                    .ToListAsync();

                apiResponse.data = products;
            }
            catch (Exception ex)
            {
                apiResponse.Message = "Something went wrong while searching for products";
                apiResponse.Status = (Byte)StatusFlag.Failed;
            }
            return apiResponse;
        }
    }
}
