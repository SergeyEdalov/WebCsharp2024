using Homework_4._1.Models.DTO;

namespace Homework_4._1.Abstractions
{
    public interface IStorageService
    {
        public IEnumerable<StorageDTO> GetStorages();
        public int AddStorage(StorageDTO storage);
    }
}
