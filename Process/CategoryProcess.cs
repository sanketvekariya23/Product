using Microsoft.EntityFrameworkCore;
using Product.Data;
using Product.Model;
using static Product.Model.Common;

namespace Product.Process
{
    public class CategoryProcess
    {
        public async Task<ApiResponse> Get()
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                apiResponse.data = await defaultContext.Category.AsNoTracking().ToListAsync();
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in getting Category data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Create(Category category)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                await defaultContext.Category.AddAsync(category);
                await defaultContext.SaveChangesAsync();
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in Creating Category data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Update(int id, Category category)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                var categoryexist = await defaultContext.Category.FindAsync(id);
                if (categoryexist != null)
                {
                    defaultContext.Category.Update(category);
                    await defaultContext.SaveChangesAsync();
                }
                else
                {
                    apiResponse.Message = "Category not found";
                    apiResponse.Status = (Byte)StatusFlag.Failed;
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in Updating Category data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Delete(int categoryid)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                Category? categoryexist = defaultContext.Category.AsNoTracking().FirstOrDefault(t => t.CategoryId == categoryid);
                if (categoryexist != null)
                {
                    defaultContext.Category.Remove(categoryexist);
                    await defaultContext.SaveChangesAsync();
                }
                else
                {
                    apiResponse.Status = (Byte)StatusFlag.Failed;
                    apiResponse.Message = "Category not found";
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in deleting Category data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
    }
}
