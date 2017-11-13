using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Contracts;
using Core.Contracts;

namespace ChefsBook_UWP_App.Services
{
    public class RecipeApiService : IRecipeApiService
    {
        private FakeRecipeApiService _fakeApiSerivce = new FakeRecipeApiService();

        public Task<List<RecipeDTO>> GetAllRecipes()
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

        public Task<List<TagDTO>> GetAllTags()
        {
            return _fakeApiSerivce.GetAllTags();
        }

        public Task<List<RecipeDetailsDTO>> FilterRecipes(FilterRecipeDTO filter)
        {
            return _fakeApiSerivce.FilterRecipes(filter);
        }
    }
}
