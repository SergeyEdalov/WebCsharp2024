using AutoMapper;
using Homework_3.Models.DTO;
using Microsoft.Extensions.Caching.Memory;
using Homework_3.Abstractions;

namespace Homework_3.Services
{
    public class StorageInfo : IStorageInfo
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public StorageInfo(AppDbContext appDbContext, IMapper mapper, IMemoryCache cache)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _cache = cache;
        }

        public IEnumerable<ProductDTO> GetProductsInStorage(int idStorage)
        {
            using (_appDbContext)
            {
                if (_cache.TryGetValue("productsInStorage", out List<ProductDTO>? products))
                    return products;

                products = _appDbContext.Products
                    .Where(x => x.Id == idStorage)
                    .Select(x => _mapper.Map<ProductDTO>(x))
                    .ToList();

                _cache.Set("productsInStorage", products, TimeSpan.FromMinutes(30));

                return products;
            }
        }
    }
}
