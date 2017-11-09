using ChefsBook_UWP_App.ViewModels;
using System;
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

            var recipeVM = this.DataContext as RecipeEditViewModel;
            if (e.Parameter == null)
            {
                recipeVM.Recipe = new RecipeViewModel();
                return;
            }

            recipeVM.Recipe = (e.Parameter as RecipeViewModel);
        }

        private void AcceptAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var recipeVM = this.DataContext as RecipeEditViewModel;
            recipeVM.SaveRecipeCommand.Execute(null);
            Frame.Navigate(typeof(RecipeCollectionPage));
        }

        private void CancelAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RecipeCollectionPage));
        }
    }
}
