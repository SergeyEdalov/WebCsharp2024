using Microsoft.AspNetCore.Mvc;
using Homework_2.Models.Repositories;
using Homework_2.Models;
using Homework_2.Abstractions;

namespace Homework_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private IProductRepository _productRepository;

        public CategoriesController(IProductRepository repository)
        {
            _productRepository = repository;
        }

        [HttpGet("getCategory")]
        public IActionResult GetCategories()
        {
            var categories = _productRepository.GetCategories();
            return Ok(categories);
        }

        [HttpPost("putCategory")]
        public IActionResult PutCategories([FromQuery] string name, string description, List<Product> products)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (!context.Categories.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        context.Add(new Category()
                        {
                            Name = name,
                            Description = description,
                            Products = products
                        });
                        context.SaveChanges();
                        return Ok();
                    }
                    else return StatusCode(409);
                }
            }
            catch { return StatusCode(500); }
        }

        [HttpDelete("deleteCategory")]
        public IActionResult DeleteCategories(string name)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Categories.Any(x => x.Name.Equals(name)))
                    {
                        context.Categories.Remove((Category)context.Categories.Select(x => x.Name));

                        context.SaveChanges();
                        return Ok();
                    }
                    else return StatusCode(404);
                }
            }
            catch { return StatusCode(500); }
        }
    }
}
