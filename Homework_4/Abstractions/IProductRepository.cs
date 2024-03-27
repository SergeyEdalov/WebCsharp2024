using Homework_4.Models.DTO;

namespace Homework_4.Abstractions
{
    public interface IProductRepository
    {
        public int AddCategory(CategoryDTO category);
        public IEnumerable<CategoryDTO> GetCategories();
        public int AddProduct(ProductDTO product);
        public IEnumerable<ProductDTO> GetProducts();
    }
}
