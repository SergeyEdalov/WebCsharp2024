using Microsoft.AspNetCore.Mvc;
using Seminar_1.Models;
using Seminar_1.Models.Repositories;

namespace Seminar_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("getProduct")]
        public IActionResult GetProducts()
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var products = context.Products.Select(x => new Product()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Cost = x.Cost,
                        CategoryId = x.CategoryId,
                        Category = x.Category,
                        Storages = x.Storages
                    });
                    return Ok(products);
                }
            }
            catch { return StatusCode(500); }
        }

        [HttpPost("putProduct")]
        public IActionResult PutProducts([FromQuery] string name, string description, int categoryId, int cost)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (!context.Products.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        context.Add(new Product()
                        {
                            Name = name,
                            Description = description,
                            Cost = cost,
                            CategoryId = categoryId
                        });
                        context.SaveChanges();
                        return Ok();
                    }
                    else return StatusCode(409);
                }
            }
            catch { return StatusCode(500); }
        }

        [HttpDelete("deleteProduct")]
        public IActionResult DeleteProducts(string name)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Products.Any(x => x.Name.Equals(name)))
                    {
                        context.Products.Remove((Product)context.Products.Select(x => x.Name));

                        context.SaveChanges();
                        return Ok();
                    }
                    else return StatusCode(404);
                }
            }
            catch { return StatusCode(500); }
        }

        [HttpPut("SetCostProduct")]
        public IActionResult SetCostProducts(string name, int cost)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var product = context.Products.Where(x => x.Name.Equals(name))
                        .FirstOrDefault();

                    if (product != null)
                    {
                        product.Cost = cost;

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
