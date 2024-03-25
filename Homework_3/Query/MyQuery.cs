using Homework_3.Abstractions;
using Homework_3.Models.DTO;

namespace Homework_3.Query
{
    public class MyQuery
    {
        public IEnumerable<ProductDTO> GetProducts([Service] IProductService service)
            => service.GetProducts();
        public IEnumerable<ProductDTO> GetProductsInStorage([Service] IStorageInfo serviceInfo, int idStorage)
            => serviceInfo.GetProductsInStorage(idStorage);
        public IEnumerable<StorageDTO> GetStorages([Service] IStorageService service)
            => service.GetStorages();
        public IEnumerable<CategoryDTO> GetCategories([Service] ICategoryService service)
            => service.GetCategories();

    }
}
