using ChefsBook.Core.Contracts;
using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeDetailsPageViewModel : ViewModelBase
    {
        private readonly IRecipeApiService _recipeApiService;

        public RecipeDetailsPageViewModel(IRecipeApiService recipeApiService)
        {
            _recipeApiService = recipeApiService;
            if (IsInDesignMode)
            {
                var task = _recipeApiService.GetRecipe(new Guid("4b602f03-5da9-4450-9946-6b248d26b142"));
                task.Wait();
                Recipe = new RecipeDetailsViewModel(task.Result);
            }
        }
        public void GetRecipeDetails(Guid id)
        {
            if (IsInDesignMode)
                return;

            var task = _recipeApiService.GetRecipe(id);
            Recipe = new RecipeDetailsViewModel(task.Result);
        }

        private RecipeDetailsViewModel _recipe;
        public RecipeDetailsViewModel Recipe
        {
            get => _recipe;
            set => Set(ref _recipe, value);
        }

        private RelayCommand _deleteRecipeCommand;
        public RelayCommand DeleteRecipeCommand
        {
            get
            {
                return _deleteRecipeCommand
                    ?? (_deleteRecipeCommand = new RelayCommand(
                    () =>
                    {
                        _recipeApiService.DeleteRecipe((RecipeDetailsDTO)Recipe);
                    }));
            }
        }
    }
}
