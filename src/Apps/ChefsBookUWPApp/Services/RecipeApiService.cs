using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Contracts;

namespace ChefsBook_UWP_App.Services
{
    public class RecipeApiService : IRecipeApiService
    {
        private FakeRecipeApiService _fakeApiSerivce = new FakeRecipeApiService();

        public Task<List<RecipeDTO>> GetAllRecipes()
        {
            return _fakeApiSerivce.GetAllRecipes();
        }

        public Task<RecipeDTO> GetRecipe(Guid id)
        {
            return _fakeApiSerivce.GetRecipe(id);
        }

        public Task AddRecipe(RecipeDTO recipe)
        {
            return _fakeApiSerivce.AddRecipe(recipe);
        }

        public Task EditRecipe(RecipeDTO recipe)
        {
            return _fakeApiSerivce.EditRecipe(recipe);
        }

        public Task DeleteRecipe(RecipeDTO recipe)
        {
            return _fakeApiSerivce.DeleteRecipe(recipe);
        }
    }
}
