using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsBook.Core.Repositories
{
    public interface IRecipesRepository
    {
        void Add(Recipe recipe);
        Task<List<Recipe>> AllAsync();
        Task<List<Recipe>> AllByUserAsync(Guid userId);
        Task<List<Recipe>> FilterAsync(Guid userId, string text, IList<string> tags);
        Task<Recipe> FindAsync(Guid recipeId);
    }
}