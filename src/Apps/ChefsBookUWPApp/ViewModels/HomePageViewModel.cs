using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ChefsBook_UWP_App.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        public HomePageViewModel(IApiService apiService)
        {
            _apiService = apiService;
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
                        _apiService.LoadSampleData();
                    }));
            }
        }
    }
}
