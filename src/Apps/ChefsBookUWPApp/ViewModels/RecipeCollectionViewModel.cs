using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeCollectionViewModel : ViewModelBase
    {
        private ObservableCollection<RecipeViewModel> _recipes;
        public ObservableCollection<RecipeViewModel> Recipes
        {
            get => _recipes;
            set => Set(() => Recipes, ref _recipes, value);
        }
    }
}
