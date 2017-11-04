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
        void Update(Recipe recipe);
        void Remove(Recipe recipe);
        Task<List<Recipe>> AllAsync();
        Task<Recipe> FindAsync(Guid recipeId);
    }
}