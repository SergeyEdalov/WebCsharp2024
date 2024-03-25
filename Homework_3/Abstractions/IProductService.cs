 using Homework_3.Models.DTO;

namespace Homework_3.Abstractions
{
    public interface IProductService
    {
        public IEnumerable<ProductDTO> GetProducts();
        public int AddProduct(ProductDTO product);

    }
}
