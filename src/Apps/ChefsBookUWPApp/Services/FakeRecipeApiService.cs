using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Contracts;

namespace ChefsBook_UWP_App.Services
{
    public class FakeRecipeApiService : IRecipeApiService
    {
        public Task<List<RecipeDTO>> GetAllRecipes()
        {
            List<RecipeDTO> recipes = new List<RecipeDTO>()
            {
                new RecipeDTO
                {
                    Id = Guid.NewGuid(),
                    Title = "Moule the crema with oreiv lemoinaie"
                },
                new RecipeDTO
                {
                    Id = Guid.NewGuid(),
                    Title = "Bacon Cheese Spread with Carmelized Onions"
                }
            };

            return Task.FromResult(recipes);
        }
    }
}
