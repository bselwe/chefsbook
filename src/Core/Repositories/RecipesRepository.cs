using System;
using System.Collections.Generic;
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

        public void Update(Recipe recipe)
        {
            dbContext.Recipes.Update(recipe);
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

        public Task<Recipe> FindAsync(Guid recipeId)
        {
            return dbContext.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .Include(r => r.Tags)
                .ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(r => r.Id == recipeId);
        }
    }
}