using ChefsBook.Auth.Contracts;
using ChefsBook.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChefsBook_UWP_App.Services
{
    public interface IApiService
    {
        Task<UserInfoDTO> SignIn(string accessToken);

        Task LoadSampleData();
        Task<List<RecipeDTO>> GetAllRecipes();
        Task<RecipeDetailsDTO> GetRecipe(Guid id);
        Task AddRecipe(RecipeDetailsDTO recipe);
        Task EditRecipe(RecipeDetailsDTO recipe);
        Task DeleteRecipe(RecipeDetailsDTO recipe);

        Task<List<TagDTO>> GetAllTags();
        Task<List<RecipeDetailsDTO>> FilterRecipes(FilterRecipeDTO filter);
    }
}
