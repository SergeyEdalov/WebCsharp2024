using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Homework_4._1.Abstractions;
using Homework_4._1.Models;
using Homework_4._1.Models.DTO;

namespace Homework_4._1.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductService(AppDbContext appDbContext, IMapper mapper, IMemoryCache cache)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _cache = cache;
        }
        //[UseDbContext()]
        public int AddProduct(ProductDTO product)
        {
            using (_appDbContext)
            {
                var entity = _mapper.Map<ProductEntity>(product);

                _appDbContext.Products.Add(entity);
                _appDbContext.SaveChanges();
                _cache.Remove("products");

                return entity.Id;
            }
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            using (_appDbContext)
            {
                if (_cache.TryGetValue("products", out List<ProductDTO> products))
                    return products;

                products = _appDbContext.Products.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
                _cache.Set("products", products, TimeSpan.FromMinutes(30));
                
                return products;
            }
        }
    }
}
