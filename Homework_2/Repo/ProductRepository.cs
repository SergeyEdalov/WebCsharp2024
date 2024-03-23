using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Homework_2.Abstractions;
using Homework_2.Models;
using Homework_2.Models.DTO;
using Homework_2.Models.Repositories;
using System.Text;

namespace Homework_2.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ProductContext _productContext;

        public ProductRepository(IMapper mapper, IMemoryCache memoryCache, ProductContext productContext)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _productContext = productContext;
        }
        public int AddCategory(CategoryDTO category)
        {
            using (_productContext)
            {
                var entityCategory = _productContext.Categories.FirstOrDefault(x => x.Name.ToLower() == category.Name.ToLower());
                if (entityCategory == null)
                {
                    entityCategory = _mapper.Map<Category>(category);
                    _productContext.Categories.Add(entityCategory);
                    _productContext.SaveChanges();
                    _memoryCache.Remove("categories");
                }
                return entityCategory.Id;
            }
        }

        public int AddProduct(ProductDTO product)
        {
            using (_productContext)
            {
                var entityProduct = _productContext.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (entityProduct == null)
                {
                    entityProduct = _mapper.Map<Product>(product);
                    _productContext.Products.Add(entityProduct);
                    _productContext.SaveChanges();
                    _memoryCache.Remove("products");
                }
                return entityProduct.Id;
            }
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            if (_memoryCache.TryGetValue("categories", out List<CategoryDTO>? categories))
                return categories;

            using (_productContext)
            {
                var listCategories = _productContext.Categories.Select(x => _mapper.Map<CategoryDTO>(x)).ToList();
                _memoryCache.Set("categories", listCategories, TimeSpan.FromMinutes(30));
                return listCategories;
            }
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            if (_memoryCache.TryGetValue("products", out List<ProductDTO>? products))
                return products;

            using (_productContext)
            {
                var listProducts = _productContext.Products.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
                _memoryCache.Set("products", listProducts, TimeSpan.FromMinutes(30));
                return listProducts;
            }
        }

        public string GetCsv(List<ProductDTO> products)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var product in products)
            {
                sb.AppendLine(product.Name + ";" + product.Description + "\n");
            }
            return sb.ToString();
        }
        public string GetCacheProductsAndCategories() { return _memoryCache.ToString(); }
    }
}
