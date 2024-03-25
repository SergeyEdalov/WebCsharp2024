namespace Homework_3.Models
{
    public class ProductEntity
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public int? StorageId { get; set; }
        public virtual CategoryEntity? Category { get; set; }
        public virtual StorageEntity? Storage { get; set; }
    }
}
