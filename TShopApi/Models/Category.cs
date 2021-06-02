using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TShopApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }

    public class CategoryValidator : AbstractValidator<Category>
    {
        private readonly TShopDBContext _dbContext;
        public CategoryValidator(TShopDBContext dBContext)
        {
            _dbContext = dBContext;

            RuleFor(c => c.Code)
                .NotEmpty()
                .MaximumLength(10)
                .Must(UniqueCode)
                .WithMessage("The code is already exists");
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(50);
        }

        private bool UniqueCode(Category category, string code)
        {
            var existingCategory = _dbContext.Categories
                                        .Where(c => c.Code.ToLower() == code.ToLower())
                                        .SingleOrDefault();

            if (existingCategory == null) return true;

            return existingCategory.Id == category.Id;
        }
    }
}
