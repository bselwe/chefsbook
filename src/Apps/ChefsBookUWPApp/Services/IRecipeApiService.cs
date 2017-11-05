using ChefsBook.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChefsBook_UWP_App.Services
{
    public interface IRecipeApiService
    {
        Task<List<RecipeDTO>> GetAllRecipes();
        Task<RecipeDTO> GetRecipe(Guid id);
        Task AddRecipe(RecipeDTO recipe);
        Task EditRecipe(RecipeDTO recipe);
        Task DeleteRecipe(RecipeDTO recipe);
    }
}
