using ChefsBook_UWP_App.ViewModels;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ChefsBook_UWP_App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecipeCollectionPage : Page
    {
        public RecipeCollectionPage()
        {
            this.InitializeComponent();
        }

        private void RecipesGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedRecipe = e.ClickedItem as RecipeViewModel;
            if (clickedRecipe != null)
                Frame.Navigate(typeof(RecipeDetailsPage), clickedRecipe.Id);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (Frame.CanGoBack)
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            else
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }
    }
}
