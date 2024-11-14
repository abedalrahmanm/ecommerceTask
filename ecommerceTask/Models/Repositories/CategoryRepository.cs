using ecommerceTask.Data;
using ecommerceTask.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ecommerceTask.Models.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly AppDbContext dbContext;

        public CategoryRepository(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void Create(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Received a null Category entity.");
            }

            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            dbContext.Categories.Add(entity);
            dbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            var category = dbContext.Categories
                .Include(c => c.SubCategories) 
                .FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                Console.WriteLine($"No category found with Id({id}).");
                return false;
            }

            if (category.SubCategories.Any())
            {
                Console.WriteLine($"Cannot delete category with Id({id}) because it has subcategories.");
                return false;
            }

            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
            return true;
        }

        public List<Category> GetAll()
        {
            try
            {
                return dbContext.Categories
                    .Include(c => c.ParentCategory)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching categories: {e.Message}");
                throw;
            }
        }

        public Category GetById(int id)
        {
            var category = dbContext.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.SubCategories)
                .FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                Console.WriteLine($"No category found with Id({id}).");
                throw new InvalidOperationException($"No category with Id({id}) found.");
            }

            return category;
        }

        public void Update(int id, Category updatedCategory)
        {
            if (updatedCategory == null)
            {
                throw new ArgumentNullException(nameof(updatedCategory), "Received a null Category entity to update.");
            }

            var existingCategory = GetById(id);
            if (existingCategory != null)
            {
                existingCategory.Name = updatedCategory.Name;
                existingCategory.Slung = updatedCategory.Slung;
                existingCategory.Description = updatedCategory.Description;
                existingCategory.Tags = updatedCategory.Tags;
                existingCategory.ParentCategoryId = updatedCategory.ParentCategoryId;
                existingCategory.UpdatedAt = DateTime.Now;

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine($"No category found with Id({id}) to update.");
            }
        }
    }
}
