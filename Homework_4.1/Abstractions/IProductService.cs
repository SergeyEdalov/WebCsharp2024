 using Homework_4._1.Models.DTO;

namespace Homework_4._1.Abstractions
{
    public interface IProductService
    {
        public IEnumerable<ProductDTO> GetProducts();
        public int AddProduct(ProductDTO product);

    }
}
