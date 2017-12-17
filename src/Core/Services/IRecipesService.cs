using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Models;

namespace ChefsBook.Core.Services
{
    public interface IRecipesService
    {
        Task CreateAsync(Guid userId, string title, string description, string image, TimeSpan? duration, int? servings, string notes, 
                    IList<Ingredient> ingredients, IList<Step> steps, IList<Tag> tags);
        Task<bool> UpdateAsync(Guid recipeId, Guid userId, string title, string description, string image, TimeSpan? duration, int? servings, string notes,
                    IList<Ingredient> ingredients, IList<Step> steps, IList<Tag> tags);
        Task<bool> RemoveAsync(Guid recipeId, Guid userId);
        Task<List<Recipe>> AllAsync();
        Task<List<Recipe>> FilterAsync(string text, IList<string> tags);
        Task<Recipe> FindAsync(Guid recipeId);
    }
}