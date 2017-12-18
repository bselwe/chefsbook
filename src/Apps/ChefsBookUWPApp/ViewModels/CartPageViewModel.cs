using ChefsBook.Core.Contracts;
using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChefsBook_UWP_App.ViewModels
{
    public class CartPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        public CartPageViewModel(IApiService apiService)
        {
            _apiService = apiService;
            GetCartItems();
        }

        private ObservableCollection<IngredientViewModel> _items = new ObservableCollection<IngredientViewModel>();
        public ObservableCollection<IngredientViewModel> Items
        {
            get => _items;
            set => Set(ref _items, value);
        }

        public async void GetCartItems()
        {
            var itemsLists = await _apiService.GetCart();
            Items.Clear();
            foreach (var itemList in itemsLists)
            {
                foreach (var item in itemList)
                {
                    Items.Add(new IngredientViewModel(item));
                }
            }
            var result = from i in Items
                    orderby i.Name
                    select i;

            Items = new ObservableCollection<IngredientViewModel>(result);
        }

        private RelayCommand _deleteCartCommand;
        public RelayCommand DeleteCartCommand
        {
            get
            {
                return _deleteCartCommand
                    ?? (_deleteCartCommand = new RelayCommand(
                    () =>
                    {
                        DeleteCart();
                    }));
            }
        }

        public async void DeleteCart()
        {
            await _apiService.DeleteCart();
            GetCartItems();
        }
    }
}
