using Homework_3.Models.DTO;

namespace Homework_3.Abstractions
{
    public interface ICategoryService
    {
        public IEnumerable<CategoryDTO> GetCategories();
        public int AddCategory(CategoryDTO category);
    }
}
