using Microsoft.AspNetCore.Mvc;
using Homework_4.Abstractions;
using Homework_4.Models;
using Homework_4.Models.DTO;
using Homework_4.Models.Repositories;

namespace Homework_4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository repository)
        {
            _productRepository = repository;
        }

        [HttpGet("get_product")]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpPost("put_products")]
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

        [HttpPost("add_product")]
        public IActionResult AddProduct([FromBody] ProductDTO productDTO)
        {
            var result = _productRepository.AddProduct(productDTO);
            return Ok(result);
        }



        [HttpDelete("delete_product")]
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
