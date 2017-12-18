using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Contracts;
using Windows.Storage;
using Newtonsoft.Json;
using ChefsBook.Auth.Contracts;

namespace ChefsBook_UWP_App.Services
{
    public class ApiService : IApiService
    {
        private ApiHelper _recipesApiHelper;
        private ApiHelper _userApiHelper;

        public async Task<UserInfoDTO> SignIn(string accessToken)
        {
            _recipesApiHelper = new ApiHelper("https://localhost:5001/api/", accessToken);
            _userApiHelper = new ApiHelper("https://localhost:5000/api/", accessToken);

            return await _userApiHelper.GetAsync<UserInfoDTO>("account");
        }

        public async Task LoadSampleData()
        {
            var jsonFilePath = new Uri(@"ms-appx:///Assets/sample_data.json");
            var jsonFile = await StorageFile.GetFileFromApplicationUriAsync(jsonFilePath);
            var jsonText = await FileIO.ReadTextAsync(jsonFile);
            var recipesData = JsonConvert.DeserializeObject<List<RecipeDetailsDTO>>(jsonText);

            foreach (var recipe in recipesData)
            {
                await AddRecipe(recipe);
            }
        }

        public async Task<List<RecipeDTO>> GetAllRecipes()
        {
            return await _recipesApiHelper.GetAsync<List<RecipeDTO>>("recipes");
        }

        public async Task<RecipeDetailsDTO> GetRecipe(Guid id)
        {
            return await _recipesApiHelper.GetAsync<RecipeDetailsDTO>($"recipes/{id}");
        }

        public async Task AddRecipe(RecipeDetailsDTO recipe)
        {
            await _recipesApiHelper.PostAsync("recipes", recipe);
        }

        public async Task EditRecipe(RecipeDetailsDTO recipe)
        {
            await _recipesApiHelper.PutAsync($"recipes/{recipe.Id}", recipe);
        }

        public async Task DeleteRecipe(RecipeDetailsDTO recipe)
        {
            await _recipesApiHelper.DeleteAsync($"recipes/{recipe.Id}");
        }

        public async Task<List<TagDTO>> GetAllTags()
        {
            return await _recipesApiHelper.GetAsync<List<TagDTO>>("tags");
        }

        public async Task<List<RecipeDetailsDTO>> FilterRecipes(FilterRecipeDTO filter)
        {
            return await _recipesApiHelper.PostAsync<List<RecipeDetailsDTO>>("recipes/filter", filter);
        }

        public async Task<List<List<IngredientDTO>>> GetCart()
        {
            return await _recipesApiHelper.GetAsync<List<List<IngredientDTO>>>("cart");
        }

        public async Task AddRecipeToCart(RecipeDetailsDTO recipe)
        {
            await _recipesApiHelper.PostAsync($"cart/{recipe.Id}");
        }

        public async Task DeleteCart()
        {
            await _recipesApiHelper.DeleteAsync("cart");
        }
    }
}
