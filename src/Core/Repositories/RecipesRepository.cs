using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefsBook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsBook.Core.Repositories
{
    public class RecipesRepository : IRecipesRepository
    {
        private readonly CoreDbContext dbContext;
        private readonly CoreUnitOfWork unitOfWork;

        public RecipesRepository(CoreDbContext dbContext, CoreUnitOfWork unitOfWork)
        {
            this.dbContext = dbContext;
            this.unitOfWork = unitOfWork;
        }

        public void Add(Recipe recipe)
        {
            dbContext.Recipes.Add(recipe);
        }

        public void Remove(Recipe recipe)
        {
            dbContext.Recipes.Remove(recipe);
        }

        public Task<List<Recipe>> AllAsync()
        {
            return dbContext.Recipes
                .Include(r => r.Tags)
                .ThenInclude(t => t.Tag)
                .ToListAsync();
        }

        public Task<List<Recipe>> AllByUserAsync(Guid userId)
        {
            return dbContext.Recipes
                .Include(r => r.Tags)
                .ThenInclude(t => t.Tag)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public Task<List<Recipe>> FilterAsync(string text, IList<string> tags)
        {
            return dbContext.Recipes
                .Include(r => r.Tags)
                .ThenInclude(t => t.Tag)
                .Where(r =>
                    ((text == null || 
                      text.Length == 0 || 
                      r.Title.ToLower().Contains(text.ToLower()) ||
                      r.Description.ToLower().Contains(text.ToLower())) 
                      &&
                     (tags == null || 
                      tags.Count == 0 || 
                      r.Tags.Select(t => t.Tag.Name.ToLower()).Intersect(tags.Select(t => t.ToLower())).Any())))
                .ToListAsync();
        }

        public Task<Recipe> FindAsync(Guid recipeId)
        {
            return dbContext.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .Include(r => r.Tags)
                .ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(r => r.RecipeId == recipeId);
        }
    }
}