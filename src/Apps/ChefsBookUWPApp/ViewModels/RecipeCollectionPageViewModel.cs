using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System;
using GalaSoft.MvvmLight.Command;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeCollectionPageViewModel : ViewModelBase
    {
        private readonly IRecipeApiService _recipeApiService;

        public RecipeCollectionPageViewModel(IRecipeApiService recipeApiService)
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

            Recipes.Clear();
            foreach (var recipe in task.Result)
            {
                Recipes.Add(new RecipeTileViewModel(recipe));
            }
        }

        private ObservableCollection<RecipeTileViewModel> _recipes = new ObservableCollection<RecipeTileViewModel>();
        public ObservableCollection<RecipeTileViewModel> Recipes
        {
            get => _recipes;
            set => Set(ref _recipes, value);
        }

        private RelayCommand _reloadCommand;
        public RelayCommand ReloadCommand
        {
            get
            {
                return _reloadCommand
                    ?? (_reloadCommand = new RelayCommand(
                    () =>
                    {
                        GetAllRecipes();
                    }));
            }
        }
    }
}
