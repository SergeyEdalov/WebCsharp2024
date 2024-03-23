using Microsoft.AspNetCore.Mvc;
using Homework_2.Abstractions;
using Homework_2.Models;
using Homework_2.Models.DTO;
using Homework_2.Models.Repositories;
using System.Text;

namespace Homework_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;

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


        [HttpGet(template: "GetProductsCsv")]
        public FileContentResult GetProductsCsv(string name)
        {
            var products = _productRepository.GetProducts().ToList();
            var content = _productRepository.GetCsv(products);

            return File(new UTF8Encoding().GetBytes(content), "HM_1/csv", "report.csv");
        }


        [HttpGet("getMemoryCache")]
        public ActionResult<string> getMemoryCache()
        {
            string? fileName = null;
            var content = _productRepository.GetCacheProductsAndCategories();

            fileName = "cache" + DateTime.Now.ToBinary().ToString() + ".csv";

            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(),
                "StaticWorkCache", fileName), content);

            return "https://" + Request.Host.ToString() + "/static/" + fileName;
        }
    }
}
