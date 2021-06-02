using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TShopApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool OnDiscount { get; set; }
        public decimal DiscountPrice { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class ProductValidator : AbstractValidator<Product>
    {
        private readonly TShopDBContext _dbContext;

        public ProductValidator(TShopDBContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(p => p.Code)
                .NotEmpty()
                .MaximumLength(15)
                .Must(UniqueCode)
                .WithMessage("The product code already exists");
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(150);
            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");
            When(p => p.OnDiscount, () =>
            {
                RuleFor(p => p.DiscountPrice)
                    .NotEmpty()
                    .WithMessage("Discount price is required when On Discount flag is active")
                    .GreaterThan(0)
                    .WithMessage("Discount price must be greater than 0")
                    .LessThan(p => p.Price)
                    .WithMessage($"Discount price must be less than price");
            });
            RuleFor(p => p.CategoryId)
                .NotEmpty()
                .Must(ExistingCategory)
                .WithMessage("The category doesn't exists");
        }

        private bool ExistingCategory(Product product, int categoryId)
        {
            var existingCategory = _dbContext.Categories
                                .Where(c => c.Id == categoryId)
                                .FirstOrDefault();

            return existingCategory != null;
        }

        private bool UniqueCode(Product product, string code)
        {
            var existingProduct = _dbContext.Products
                                .Where(p => p.Code.ToLower() == code.ToLower())
                                .SingleOrDefault();

            if (existingProduct == null) return true;

            return existingProduct.Id == product.Id;
        }
    }
}
