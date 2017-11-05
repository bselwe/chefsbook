using ChefsBook.Core.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChefsBook_UWP_App.Services
{
    public interface IRecipeApiService
    {
        Task<List<RecipeDTO>> GetAllRecipes();
    }
}
