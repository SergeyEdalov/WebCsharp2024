using Homework_3.Abstractions;
using Homework_3.Models.DTO;

namespace Homework_3.Mutation
{
    public class MyMutation
    {
        public int AddProduct([Service] IProductService service, ProductDTO product)
        {
            int id = service.AddProduct(product);
            return id;
        }
    }
}
