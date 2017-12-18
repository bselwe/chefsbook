using ChefsBook_UWP_App.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ChefsBook_UWP_App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecipeDetailsPage : Page
    {
        private RecipeDetailsPageViewModel ViewModel { get; set; } = ServiceLocator.Current.GetInstance<RecipeDetailsPageViewModel>();

        public RecipeDetailsPage()
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
                return;

            var selectedRecipeId = (Guid)e.Parameter;
            ViewModel.GetRecipeDetails(selectedRecipeId);
        }

        private void EditRecipeAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RecipeEditPage), ViewModel.Recipe);
        }

        private void DeleteRecipeAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteRecipeCommand.Execute(null);
            Frame.Navigate(typeof(RecipeCollectionPage), true);
        }

        private void AddToCartAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            ViewModel.AddToCartCommand.Execute(null);
        }
    }
}
