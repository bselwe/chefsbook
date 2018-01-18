using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Models;

namespace ChefsBook.Core.Services
{
    public interface IRecipesService
    {
        Task CreateAsync(Guid userId, string title, bool isPrivate, string description, string image, TimeSpan? duration, int? servings, string notes, 
                    IList<Ingredient> ingredients, IList<Step> steps, IList<Tag> tags);
        Task<bool> UpdateAsync(Guid recipeId, Guid userId, string title, bool isPrivate, string description, string image, TimeSpan? duration, int? servings, string notes,
                    IList<Ingredient> ingredients, IList<Step> steps, IList<Tag> tags);
        Task<bool> RemoveAsync(Guid recipeId, Guid userId);
        Task<List<Recipe>> AllPublicAsync();
        Task<List<Recipe>> AllPublicByUserAsync(Guid userId);
        Task<List<Recipe>> AllByUserAsync(Guid userId);
        Task<List<Recipe>> FilterAsync(string text, IList<string> tags);
        Task<Recipe> FindAsync(Guid recipeId);
    }
}