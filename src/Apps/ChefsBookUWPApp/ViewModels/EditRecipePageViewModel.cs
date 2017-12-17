using ChefsBook.Core.Contracts;
using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChefsBook_UWP_App.ViewModels
{
    public class EditRecipePageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        public EditRecipePageViewModel(IApiService apiService)
        {
            _apiService = apiService;
            if (IsInDesignMode)
            {
                var task = _apiService.GetRecipe(new Guid("4b602f03-5da9-4450-9946-6b248d26b142"));
                task.Wait();
                Recipe = new RecipeDetailsViewModel(task.Result);
            }
        }

        private RecipeDetailsViewModel _recipe;
        public RecipeDetailsViewModel Recipe
        {
            get => _recipe;
            set
            {
                Set(ref _recipe, value);
                EditableTagsListing = Recipe.TagsListing;
            }
        }

        private string _editableTagsListing;
        public string EditableTagsListing
        {
            get => _editableTagsListing;
            set => Set(ref _editableTagsListing, value);
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
                        UpdateTagsCollection();

                        if (Recipe.Id == default(Guid))
                            _apiService.AddRecipe((RecipeDetailsDTO)Recipe);
                        else
                            _apiService.EditRecipe((RecipeDetailsDTO)Recipe);
                    }));
            }
        }

        private void UpdateTagsCollection()
        {
            var tagsListingWithoutSpaces = EditableTagsListing.Replace(' ', ',');

            string[] separators = { "," };
            var tagsNames = tagsListingWithoutSpaces.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var updatedTags = new ObservableCollection<TagViewModel>();
            foreach (var tagName in tagsNames)
            {
                updatedTags.Add(new TagViewModel() { Name = tagName });
            }

            Recipe.Tags = updatedTags;
        }
    }
}
