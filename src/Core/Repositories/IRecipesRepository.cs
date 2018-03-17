using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Models;

namespace ChefsBook.Core.Repositories
{
    public interface IRecipesRepository
    {
        void Add(Recipe recipe);
        Task<List<Recipe>> AllPublicAsync();
        Task<List<Recipe>> AllPublicByUserAsync(Guid userId);
        Task<List<Recipe>> AllByUserAsync(Guid userId);
        Task<List<Recipe>> FilterAsync(string text, IList<string> tags);
        Task<Recipe> FindAsync(Guid recipeId);
    }
}