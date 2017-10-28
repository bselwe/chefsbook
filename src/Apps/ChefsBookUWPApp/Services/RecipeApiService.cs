using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Models;

namespace ChefsBook_UWP_App.Services
{
    public class RecipeApiService : IRecipeApiService
    {
        public Task<List<Recipe>> GetAllRecipes()
        {
            List<Recipe> recipes = new List<Recipe>()
            {
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Name = "Real recipe name #1"
                },
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Name = "Real recipe name #2"
                }
            };

            return Task.FromResult(recipes);
        }
    }
}
