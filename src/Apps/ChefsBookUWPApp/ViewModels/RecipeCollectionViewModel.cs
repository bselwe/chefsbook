using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeCollectionViewModel : ViewModelBase
    {
        private readonly IRecipeApiService _recipeApiService;

        public RecipeCollectionViewModel(IRecipeApiService recipeApiService)
        {
            _recipeApiService = recipeApiService;
            GetAllRecipes();
        }

        private void GetAllRecipes()
        {
            var task = _recipeApiService.GetAllRecipes();
            if (IsInDesignMode)
            {
                task.Wait();
            }

            foreach (var recipe in task.Result)
            {
                Recipes.Add(new RecipeViewModel(recipe));
            }
        }

        private ObservableCollection<RecipeViewModel> _recipes = new ObservableCollection<RecipeViewModel>();
        public ObservableCollection<RecipeViewModel> Recipes
        {
            get => _recipes;
            set => Set(ref _recipes, value);
        }
    }
}
