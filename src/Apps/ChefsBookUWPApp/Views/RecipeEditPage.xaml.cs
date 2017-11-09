using ChefsBook_UWP_App.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ChefsBook_UWP_App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecipeEditPage : Page
    {
        private RecipeEditViewModel ViewModel { get; set; } = ServiceLocator.Current.GetInstance<RecipeEditViewModel>();

        public RecipeEditPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (Frame.CanGoBack)
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            else
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            if (e.Parameter == null)
            {
                ViewModel.Recipe = new RecipeViewModel();
                return;
            }

            ViewModel.Recipe = (e.Parameter as RecipeViewModel);
        }

        private void AcceptAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.SaveRecipeCommand.Execute(null);
            Frame.Navigate(typeof(RecipeCollectionPage), true);
        }

        private void CancelAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RecipeCollectionPage), false);
        }
    }
}
