using Microsoft.AspNetCore.Mvc;
using Homework_4.Models.Repositories;
using Homework_4.Models;

namespace Homework_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet("getCategory")]
        public IActionResult GetCategories()
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var categories = context.Categories.Select(x => new Category()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Products = x.Products,
                    });
                    return Ok(categories);
                }
            }
            catch { return StatusCode(500); }
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
