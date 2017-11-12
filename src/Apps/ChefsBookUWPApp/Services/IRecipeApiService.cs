using ChefsBook.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChefsBook_UWP_App.Services
{
    public interface IRecipeApiService
    {
        Task<List<RecipeDetailsDTO>> GetAllRecipes();
        Task<RecipeDetailsDTO> GetRecipe(Guid id);
        Task AddRecipe(RecipeDetailsDTO recipe);
        Task EditRecipe(RecipeDetailsDTO recipe);
        Task DeleteRecipe(RecipeDetailsDTO recipe);
    }
}
