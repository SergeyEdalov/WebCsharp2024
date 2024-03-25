using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Homework_3.Abstractions;
using Homework_3.Models;
using Homework_3.Models.DTO;

namespace Homework_3.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CategoryService(AppDbContext appDbContext, IMapper mapper, IMemoryCache cache)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _cache = cache;
        }
        public int AddCategory(CategoryDTO category)
        {
            using (_appDbContext)
            {
                var entity = _mapper.Map<CategoryEntity>(category);

                _appDbContext.Categories.Add(entity);
                _appDbContext.SaveChanges();
                _cache.Remove("categories");

                return entity.Id;
            }
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            using (_appDbContext)
            {
                if (_cache.TryGetValue("categories", out List<CategoryDTO> categories))
                    return categories;

                categories = _appDbContext.Categories.Select(x => _mapper.Map<CategoryDTO>(x)).ToList();
                _cache.Set("categories", categories, TimeSpan.FromMinutes(30));

                return categories;
            }
        }
    }
}
