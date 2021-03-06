﻿using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System;
using GalaSoft.MvvmLight.Command;
using ChefsBook.Core.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Threading;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeCollectionPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        public RecipeCollectionPageViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Task.Run(() => GetAllRecipes());
        }

        private async void GetAllRecipes()
        {
            var recipes = await _apiService.GetAllRecipes();

            var updatedRecipes = new ObservableCollection<RecipeTileViewModel>();
            foreach (var recipe in recipes)
            {
                updatedRecipes.Add(new RecipeTileViewModel(recipe));
            }

            await DispatcherHelper.RunAsync(() =>
            {
                Recipes = updatedRecipes;
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
                        Task.Run(() => GetAllRecipes());
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
            var filterDTO = new FilterRecipeDTO() { Text = TitleSearchQuery, Tags = new List<string>() };

            var tagsWithoutSpaces = TagsSearchQuery.Replace(' ', ',');
            string[] separators = { "," };
            var tagsNames = tagsWithoutSpaces.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (var tagName in tagsNames)
            {
                filterDTO.Tags.Add(tagName);
            }

            var result = await _apiService.FilterRecipes(filterDTO);

            Recipes = new ObservableCollection<RecipeTileViewModel>(
                result.ConvertAll(r => new RecipeTileViewModel(r as RecipeDTO)));
        }
    }
}
