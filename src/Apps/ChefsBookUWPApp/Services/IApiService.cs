using ChefsBook.Core.Contracts;
using ChefsBook_UWP_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChefsBook_UWP_App.Services
{
    public interface IApiService
    {
        Task<UserViewModel> SignIn(string accessToken);

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
