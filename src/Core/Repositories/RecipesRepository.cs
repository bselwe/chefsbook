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

        public RecipesRepository(CoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Recipe recipe)
        {
            dbContext.Recipes.Add(recipe);
        }

        public Task<List<Recipe>> AllAsync()
        {
            return dbContext.Recipes
                .Include(r => r.Tags)
                .ThenInclude(t => t.Tag)
                .Where(r => !r.IsDeleted)
                .ToListAsync();
        }

        public Task<List<Recipe>> AllByUserAsync(Guid userId)
        {
            return dbContext.Recipes
                .Include(r => r.Tags)
                .ThenInclude(t => t.Tag)
                .Where(r => !r.IsDeleted && r.UserId == userId)
                .ToListAsync();
        }

        public Task<List<Recipe>> FilterAsync(Guid userId, string text, IList<string> tags)
        {
            return dbContext.Recipes
                .Include(r => r.Tags)
                .ThenInclude(t => t.Tag)
                .Where(r =>
                    (!r.IsDeleted &&
                     (userId == null || r.UserId == userId) &&   
                     (text == null || 
                      text.Length == 0 || 
                      r.Title.ToLower().Contains(text.ToLower()) ||
                      r.Description.ToLower().Contains(text.ToLower())) &&
                     (tags == null || 
                      tags.Count == 0 || 
                      r.Tags.Select(t => t.Tag.Name.ToLower()).Intersect(tags.Select(t => t.ToLower())).Any())))
                .ToListAsync();
        }

        public async Task<Recipe> FindAsync(Guid recipeId)
        {
            var x = await dbContext.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .Include(r => r.Tags)
                .ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(r => !r.IsDeleted && r.RecipeId == recipeId);

            return Recipe.Create(
                    x.UserId, x.Title, x.Description, x.Image, x.Duration, x.Servings, x.Notes,
                    x.Ingredients.OrderBy(i => i.SequenceNumber).ToList(), x.Steps.OrderBy(s => s.SequenceNumber).ToList());
        }
    }
}