using Homework_4._1.Abstractions;
using Homework_4._1.Models.DTO;

namespace Homework_4._1.Mutation
{
    public class MyMutation
    {
        public int Addproduct([Service] IProductService service, ProductDTO product)
        {
            int id = service.AddProduct(product);
            return id;
        }
    }
}
