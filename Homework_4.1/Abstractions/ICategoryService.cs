using Homework_4._1.Models.DTO;

namespace Homework_4._1.Abstractions
{
    public interface ICategoryService
    {
        public IEnumerable<CategoryDTO> GetCategories();
        public int AddCategory(CategoryDTO category);
    }
}
