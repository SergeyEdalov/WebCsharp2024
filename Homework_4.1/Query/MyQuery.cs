using Homework_4._1.Abstractions;
using Homework_4._1.Models.DTO;

namespace Homework_4._1.Query
{
    public class MyQuery
    {
        public IEnumerable<ProductDTO> GetProducts([Service] IProductService service)
            => service.GetProducts();
        public IEnumerable<StorageDTO> GetStorages([Service] IStorageService service)
            => service.GetStorages();
        public IEnumerable<CategoryDTO> GetCategories([Service] ICategoryService service)
            => service.GetCategories();

    }
}
