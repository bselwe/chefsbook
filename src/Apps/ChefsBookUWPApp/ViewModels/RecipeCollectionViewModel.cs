using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeCollectionViewModel : ViewModelBase
    {
        private readonly IRecipeApiService _recipeApiService;

        public RecipeCollectionViewModel(IRecipeApiService recipeApiService)
        {
            _recipeApiService = recipeApiService;
        }

        private ObservableCollection<RecipeViewModel> _recipes;
        public ObservableCollection<RecipeViewModel> Recipes
        {
            get => _recipes;
            set => Set(ref _recipes, value);
        }
    }
}
