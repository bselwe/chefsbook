using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Contracts;

namespace ChefsBook_UWP_App.Services
{
    public class RecipeApiService : IRecipeApiService
    {
        private FakeRecipeApiService _fakeApiSerivce = new FakeRecipeApiService();

        public Task<List<RecipeDTO>> GetAllRecipes()
        {
            return _fakeApiSerivce.GetAllRecipes();
        }
    }
}
