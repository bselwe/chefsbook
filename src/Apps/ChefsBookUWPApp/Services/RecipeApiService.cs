using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Contracts;

namespace ChefsBook_UWP_App.Services
{
    public class RecipeApiService : IRecipeApiService
    {
        private FakeRecipeApiService _fakeApiSerivce = new FakeRecipeApiService();

        public Task<List<RecipeDetailsDTO>> GetAllRecipes()
        {
            return _fakeApiSerivce.GetAllRecipes();
        }

        public Task<RecipeDetailsDTO> GetRecipe(Guid id)
        {
            return _fakeApiSerivce.GetRecipe(id);
        }

        public Task AddRecipe(RecipeDetailsDTO recipe)
        {
            return _fakeApiSerivce.AddRecipe(recipe);
        }

        public Task EditRecipe(RecipeDetailsDTO recipe)
        {
            return _fakeApiSerivce.EditRecipe(recipe);
        }

        public Task DeleteRecipe(RecipeDetailsDTO recipe)
        {
            return _fakeApiSerivce.DeleteRecipe(recipe);
        }
    }
}
