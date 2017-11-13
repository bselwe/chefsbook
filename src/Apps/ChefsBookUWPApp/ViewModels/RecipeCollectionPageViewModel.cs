using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System;
using GalaSoft.MvvmLight.Command;
using Core.Contracts;
using ChefsBook.Core.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Threading;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeCollectionPageViewModel : ViewModelBase
    {
        private readonly IRecipeApiService _recipeApiService;

        public RecipeCollectionPageViewModel(IRecipeApiService recipeApiService)
        {
            _recipeApiService = recipeApiService;
            Task.Run(() => GetAllRecipes());
            Task.Run(() => GetAllTags());
        }

        private async void GetAllTags()
        {
            var tags = await _recipeApiService.GetAllTags();

            await DispatcherHelper.RunAsync(() =>
            {
                _availableTags = tags;
            });
        }

        private async void GetAllRecipes()
        {
            var recipes = await _recipeApiService.GetAllRecipes();

            await DispatcherHelper.RunAsync(() =>
            {
                Recipes.Clear();
                foreach (var recipe in recipes)
                {
                    Recipes.Add(new RecipeTileViewModel(recipe));
                }
            });
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

        private string _titleSearchQuery = string.Empty;
        public string TitleSearchQuery  
        {
            get => _titleSearchQuery;
            set => Set(ref _titleSearchQuery, value);
        }

        private string _tagsSearchQuery = string.Empty;
        public string TagsSearchQuery
        {
            get => _tagsSearchQuery;
            set => Set(ref _tagsSearchQuery, value);
        }

        private List<TagDTO> _availableTags { get; set; }

        private RelayCommand _searchQuerySubmittedCommand;
        public RelayCommand SearchQuerySubmittedCommand
        {
            get
            {
                return _searchQuerySubmittedCommand
                    ?? (_searchQuerySubmittedCommand = new RelayCommand(SearchQuerySubmitted));
            }
        }

        private async void SearchQuerySubmitted()
        {
            var filterDTO = new FilterRecipeDTO() { Text = TitleSearchQuery, Tags = new List<Guid>() };

            var tagsWithoutSpaces = TagsSearchQuery.Replace(' ', ',');
            string[] separators = { "," };
            var tagsNames = tagsWithoutSpaces.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (var tagName in tagsNames)
            {
                var foundTag = _availableTags.FirstOrDefault(t => t.Name == tagName);

                if (foundTag != default(TagDTO))
                    filterDTO.Tags.Add(foundTag.Id);
            }

            var result = await _recipeApiService.FilterRecipes(filterDTO);

            Recipes = new ObservableCollection<RecipeTileViewModel>(
                result.ConvertAll(r => new RecipeTileViewModel(r as RecipeDTO)));
        }
    }
}
