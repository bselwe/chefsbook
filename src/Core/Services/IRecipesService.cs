using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Models;

namespace ChefsBook.Core.Services
{
    public interface IRecipesService
    {
        Task Create(string title, string description, TimeSpan? duration, int? servings, string notes, 
                    IList<Ingredient> ingredients, IList<Step> steps, IList<Tag> tags);
        Task<bool> Update(Guid id, string title, string description, TimeSpan? duration, int? servings, string notes,
                    IList<Ingredient> ingredients, IList<Step> steps, IList<Tag> tags);
        Task<bool> Remove(Guid recipeId);
        Task<List<Recipe>> AllAsync();
        Task<Recipe> FindAsync(Guid recipeId);
    }
}