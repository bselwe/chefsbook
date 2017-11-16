using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ChefsBook_UWP_App.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IRecipeApiService _recipeApiService;

        public HomePageViewModel(IRecipeApiService recipeApiService)
        {
            _recipeApiService = recipeApiService;
        }

        private RelayCommand _loadSampleDataCommand;
        public RelayCommand LoadSampleDataCommand
        {
            get
            {
                return _loadSampleDataCommand
                    ?? (_loadSampleDataCommand = new RelayCommand(
                    () =>
                    {
                        _recipeApiService.LoadSampleData();
                    }));
            }
        }
    }
}
