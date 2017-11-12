using ChefsBook.Core.Contracts;
using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace ChefsBook_UWP_App.ViewModels
{
    public class EditRecipePageViewModel : ViewModelBase
    {
        private readonly IRecipeApiService _recipeApiService;

        public EditRecipePageViewModel(IRecipeApiService recipeApiService)
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

        private RecipeViewModel _recipe;
        public RecipeViewModel Recipe
        {
            get => _recipe;
            set => Set(ref _recipe, value);
        }

        private RelayCommand<string> _saveImagePathCommand;
        public RelayCommand<string> SaveImagePathCommand
        {
            get
            {
                return _saveImagePathCommand
                    ?? (_saveImagePathCommand = new RelayCommand<string>(
                    (name) =>
                    {
                        var localImageFolder = "ms-appdata:///local/RecipeImages/";
                        var imagePath = localImageFolder + name;
                        Recipe.Image = imagePath;
                    }));
            }
        }

        private RelayCommand _addEmptyIngredientCommand;
        public RelayCommand AddEmptyIngredientCommand
        {
            get
            {
                return _addEmptyIngredientCommand
                    ?? (_addEmptyIngredientCommand = new RelayCommand(
                    () =>
                    {
                        Recipe.Ingredients.Add(new IngredientViewModel());
                    }));
            }
        }

        private RelayCommand _addEmptyStepCommand;
        public RelayCommand AddEmptyStepCommand
        {
            get
            {
                return _addEmptyStepCommand
                    ?? (_addEmptyStepCommand = new RelayCommand(
                    () =>
                    {
                        Recipe.Steps.Add(new StepViewModel());
                    }));
            }
        }

        private RelayCommand<IngredientViewModel> _deleteIngredientCommand;
        public RelayCommand<IngredientViewModel> DeleteIngredientCommand
        {
            get
            {
                return _deleteIngredientCommand
                    ?? (_deleteIngredientCommand = new RelayCommand<IngredientViewModel>(
                    (ingredient) =>
                    {
                        Recipe.Ingredients.Remove(ingredient);
                    }));
            }
        }

        private RelayCommand<StepViewModel> _deleteStepCommand;
        public RelayCommand<StepViewModel> DeleteStepCommand
        {
            get
            {
                return _deleteStepCommand
                    ?? (_deleteStepCommand = new RelayCommand<StepViewModel>(
                    (step) =>
                    {
                        Recipe.Steps.Remove(step);
                    }));
            }
        }

        private RelayCommand _saveRecipeCommand;
        public RelayCommand SaveRecipeCommand
        {
            get
            {
                return _saveRecipeCommand
                    ?? (_saveRecipeCommand = new RelayCommand(
                    () =>
                    {
                        if (Recipe.Id == default(Guid))
                            _recipeApiService.AddRecipe((RecipeDetailsDTO)Recipe);
                        else
                            _recipeApiService.EditRecipe((RecipeDetailsDTO)Recipe);
                    }));
            }
        }
    }
}
