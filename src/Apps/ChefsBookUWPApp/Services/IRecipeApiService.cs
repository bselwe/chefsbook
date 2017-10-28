using ChefsBook.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChefsBook_UWP_App.Services
{
    public interface IRecipeApiService
    {
        Task<List<Recipe>> GetAllRecipes();
    }
}
