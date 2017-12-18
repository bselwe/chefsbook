using ChefsBook_UWP_App.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System.Text;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ChefsBook_UWP_App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CartPage : Page
    {
        private CartPageViewModel ViewModel { get; set; } = ServiceLocator.Current.GetInstance<CartPageViewModel>();

        public CartPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter == null)
                return;

            if (e.Parameter != null && (bool)e.Parameter == true)
                ViewModel.GetCartItems();
        }
    }
}
