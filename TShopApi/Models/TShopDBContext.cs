using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TShopApi.Models
{
    public class TShopDBContext : DbContext
    {
        public TShopDBContext(DbContextOptions<TShopDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region - Category Definition

            modelBuilder.Entity<Category>(
                cb =>
                {
                    cb.Property(c => c.Code).HasColumnType("nvarchar(10)").IsRequired();
                    cb.Property(c => c.Name).HasColumnType("nvarchar(50)").IsRequired();
                    cb.Property(c => c.Description).HasColumnType("nvarchar(max)");
                });

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);
            
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Code)
                .IsUnique();

            #endregion

            #region - Product Definition

            modelBuilder.Entity<Product>(
                pb =>
                {
                    pb.Property(p => p.Code).HasColumnType("nvarchar(15)").IsRequired();
                    pb.Property(p => p.Name).HasColumnType("nvarchar(150)").IsRequired();
                    pb.Property(p => p.Description).HasColumnType("nvarchar(MAX)");
                    pb.Property(p => p.Price).HasColumnType("decimal(6, 2)").IsRequired();
                    pb.Property(p => p.OnDiscount).HasColumnType("bit").IsRequired().HasDefaultValue(false);
                    pb.Property(p => p.DiscountPrice).HasColumnType("decimal(6, 2)");
                    pb.Property(p => p.ImageUrl).HasColumnType("nvarchar(200)");
                });

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Code)
                .IsUnique();

            #endregion

            #region - Relationships Definition

            modelBuilder.Entity<Product>()
                .HasOne<Category>(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            #endregion

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
