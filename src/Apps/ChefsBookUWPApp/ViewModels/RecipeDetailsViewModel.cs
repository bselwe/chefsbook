using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using System;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeDetailsViewModel : ViewModelBase
    {
        private readonly IRecipeApiService _recipeApiService;

        public RecipeDetailsViewModel(IRecipeApiService recipeApiService)
        {
            _recipeApiService = recipeApiService;
            if (IsInDesignMode)
            {
                var task = _recipeApiService.GetAllRecipes();
                task.Wait();
                var firstRecipe = task.Result[0];
                Recipe = new RecipeViewModel(firstRecipe);
            }
        }
        public void GetRecipeDetails(Guid id)
        {
            if (IsInDesignMode)
                return;

            var task = _recipeApiService.GetRecipe(id);
            Recipe = new RecipeViewModel(task.Result);
        }

        private RecipeViewModel _recipe;
        public RecipeViewModel Recipe
        {
            get => _recipe;
            set => Set(ref _recipe, value);
        }
    }
}
