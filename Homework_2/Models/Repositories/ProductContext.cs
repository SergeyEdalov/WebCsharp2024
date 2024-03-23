using Microsoft.EntityFrameworkCore;

namespace Homework_2.Models.Repositories
{
    public class ProductContext : DbContext
    {
        public DbSet<Storage> ProductStorages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        private string _connectionString;

        public ProductContext() { }
        public ProductContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.
            UseNpgsql(_connectionString).UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(x => x.Id)
                        .HasName("ProductId");
                entity.HasIndex(x => x.Name)
                        .IsUnique();

                entity.Property(e => e.Name)
                        .HasColumnName("ProductName")
                        .HasMaxLength(255)
                        .IsRequired();
                entity.Property(e => e.Description)
                        .HasColumnName("Descrition")
                        .HasMaxLength(255)
                        .IsRequired();
                entity.Property(e => e.Cost)
                        .HasColumnName("Price")
                        .IsRequired();

                entity.HasOne(x => x.Category)
                        .WithMany(c => c.Products)
                        .HasForeignKey(x => x.Id)
                        .HasConstraintName("Category");

            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("ProductsCategory");

                entity.HasKey(x => x.Id)
                        .HasName("CategoryId");
                entity.HasIndex(x => x.Name)
                        .IsUnique();

                entity.Property(e => e.Name)
                        .HasColumnName("ProductName")
                        .HasMaxLength(255)
                        .IsRequired();
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");

                entity.HasKey(x => x.Id)
                        .HasName("StorageId");

                entity.Property(e => e.Name)
                        .HasColumnName("ProductName");

                entity.HasMany(x => x.Products)
                        .WithMany(e => e.Storages)
                        .UsingEntity(j => j.ToTable("ProductStorage"));
            });
        }
    }
}
