using Homework_3.Models.DTO;

namespace Homework_3.Abstractions
{
    public interface IStorageInfo
    {
        public IEnumerable<ProductDTO> GetProductsInStorage(int idStorage);
    }
}
