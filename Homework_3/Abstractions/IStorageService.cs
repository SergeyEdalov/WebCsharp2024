using Homework_3.Models.DTO;

namespace Homework_3.Abstractions
{
    public interface IStorageService
    {
        public IEnumerable<StorageDTO> GetStorages();
        public int AddStorage(StorageDTO storage);
    }
}
