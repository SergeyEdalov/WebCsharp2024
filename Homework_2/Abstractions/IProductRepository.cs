using Homework_2.Models.DTO;

namespace Homework_2.Abstractions
{
    public interface IProductRepository
    {
        public int AddCategory(CategoryDTO category);
        public IEnumerable<CategoryDTO> GetCategories();
        public int AddProduct(ProductDTO product);
        public IEnumerable<ProductDTO> GetProducts();
        public string GetCsv(List<ProductDTO> products);
        public string GetCacheProductsAndCategories();
    }
}
