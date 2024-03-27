using AutoMapper;
using HotChocolate.Utilities;
using Microsoft.Extensions.Caching.Memory;
using Homework_4._1.Abstractions;
using Homework_4._1.Models;
using Homework_4._1.Models.DTO;

namespace Homework_4._1.Services
{
    public class StorageService : IStorageService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public StorageService(AppDbContext appDbContext, IMapper mapper, IMemoryCache cache)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _cache = cache;
        }
        public int AddStorage(StorageDTO storage)
        {
            using (_appDbContext)
            {
                var entity = _mapper.Map<StorageEntity>(storage);

                _appDbContext.ProductStorages.Add(entity);
                _appDbContext.SaveChanges();
                _cache.Remove("products");

                return entity.Id;
            }
        }

        public IEnumerable<StorageDTO> GetStorages()
        {
            using (_appDbContext)
            {
                if (_cache.TryGetValue("storages", out List<StorageDTO> storages))
                    return storages;

                storages = _appDbContext.ProductStorages.Select(x => _mapper.Map<StorageDTO>(x)).ToList();
                _cache.Set("storages", storages, TimeSpan.FromMinutes(30));

                return storages;
            }
        }
    }
}
