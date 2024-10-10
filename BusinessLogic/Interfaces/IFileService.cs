using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveProductImage(IFormFile file);
        void DeleteProductImage(string path);
        Task<string> EditProductImage(string oldPath, IFormFile newFile);
    }
}

