﻿using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory is null)
            {
                return null;
            }
            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(string? query = null, string? sortBy = null,
            string? sortDirection = null, int? pageNumber = 1, int? pageSize = 100)
        {
            // query the database
            var categories = dbContext.Categories.AsQueryable();

            // filtering
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                categories = categories.Where(x => x.Name.Contains(query));
            }

            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) 
                        ? true : false;

                    categories = isAsc ? categories.OrderBy(x => x.Name) : categories.OrderByDescending(x => x.Name);
                }

                if (string.Equals(sortBy, "URL", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase)
                        ? true : false;

                    categories = isAsc ? categories.OrderBy(x => x.UrlHandle) : categories.OrderByDescending(x => x.Name);
                }
            }

            // pagination
            // pageNumber = 1, pageSize = 5 => skip 0, show 5
            // pageNumber = 2, pageSize = 5 => skip 5, show 5  (6, 7, 8, 9, 10)
            // pageNumber = 3, pageSize = 5 => skip 10, show 5

            var skipResults = (pageNumber - 1) * pageSize;
            categories = categories.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await categories.ToListAsync();

            //return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCount() 
        {
            return await dbContext.Categories.CountAsync();
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (existingCategory != null)
            {
                dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }
            return null;
        }
    }
}
