using Microsoft.EntityFrameworkCore;
using Homework_3.Models;

namespace Homework_3
{
    public class AppDbContext : DbContext
    {
        private string _connectionString;

        public DbSet<StorageEntity> ProductStorages { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }

        public AppDbContext() { }
        public AppDbContext(string connectionString) 
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
            .UseNpgsql(_connectionString)
            .UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductEntity>(entity =>
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

                entity.HasOne(x => x.Storage)
                        .WithMany(c => c.Products)
                        .HasForeignKey(x => x.Id)
                        .HasConstraintName("Storages");

            });

            modelBuilder.Entity<CategoryEntity>(entity =>
            {
                entity.ToTable("ProductsCategory");

                entity.HasKey(x => x.Id)
                        .HasName("CategoryId");
                entity.HasIndex(x => x.Name)
                        .IsUnique();
                entity.Property(e => e.Name)
                        .HasColumnName("CategoryName")
                        .HasMaxLength(255)
                        .IsRequired();
            });

            modelBuilder.Entity<StorageEntity>(entity =>
            {
                entity.ToTable("Storage");

                entity.HasKey(x => x.Id)
                        .HasName("StorageId");

                entity.Property(e => e.Name)
                        .HasColumnName("StorageName");
            });
        }
    }
}
