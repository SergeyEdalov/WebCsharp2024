using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Homework_4.Abstractions;
using Homework_4.Models;
using Homework_4.Models.DTO;
using Homework_4.Models.Repositories;

namespace Homework_4.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public ProductRepository(IMapper mapper, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
        }
        public int AddCategory(CategoryDTO category)
        {
            using (var context = new ProductContext())
            {
                var entityCategory = context.Categories.FirstOrDefault(x => x.Name.ToLower() == category.Name.ToLower());
                if (entityCategory == null)
                {
                    entityCategory = _mapper.Map<Category>(category);
                    context.Categories.Add(entityCategory);
                    context.SaveChanges();
                    _memoryCache.Remove("categories");
                }
                return entityCategory.Id;
            }
        }

        public int AddProduct(ProductDTO product)
        {
            using (var context = new ProductContext())
            {
                var entityProduct = context.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (entityProduct == null)
                {
                    entityProduct = _mapper.Map<Product>(product);
                    context.Products.Add(entityProduct);
                    context.SaveChanges();
                    _memoryCache.Remove("products");
                }
                return entityProduct.Id;
            }
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            if (_memoryCache.TryGetValue("categories", out List<CategoryDTO>? categories))
                return categories;

            using (var context = new ProductContext())
            {
                var listCategories =  context.Categories.Select(x => _mapper.Map<CategoryDTO>(x)).ToList();
                _memoryCache.Set("categories", listCategories, TimeSpan.FromMinutes(30));
                return listCategories;
            }
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            if (_memoryCache.TryGetValue("products", out List<ProductDTO>? products))
                return products;

            using (var context = new ProductContext())
            {
                var listProducts = context.Products.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
                _memoryCache.Set("products", listProducts, TimeSpan.FromMinutes(30));
                return listProducts;
            }
        }
    }
}
