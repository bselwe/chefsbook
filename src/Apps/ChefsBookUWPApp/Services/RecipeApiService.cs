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
        private ApiHelper _apiHelper = new ApiHelper();

        public async Task<List<RecipeDTO>> GetAllRecipes()
        {
            return await _apiHelper.GetAsync<List<RecipeDTO>>("recipes");
        }

        public async Task<RecipeDetailsDTO> GetRecipe(Guid id)
        {
            return await _apiHelper.GetAsync<RecipeDetailsDTO>($"recipes/{id}");
        }

        public async Task AddRecipe(RecipeDetailsDTO recipe)
        {
            await _apiHelper.PostAsync("recipes", recipe);
        }

        public async Task EditRecipe(RecipeDetailsDTO recipe)
        {
            await _apiHelper.PutAsync($"recipes/{recipe.Id}", recipe);
        }

        public async Task DeleteRecipe(RecipeDetailsDTO recipe)
        {
            await _apiHelper.DeleteAsync($"recipes/{recipe.Id}");
        }

        public async Task<List<TagDTO>> GetAllTags()
        {
            return await _apiHelper.GetAsync<List<TagDTO>>("tags");
        }

        public async Task<List<RecipeDetailsDTO>> FilterRecipes(FilterRecipeDTO filter)
        {
            return await _apiHelper.PostAsync<List<RecipeDetailsDTO>>("recipes/filter", filter);
        }
    }
}
