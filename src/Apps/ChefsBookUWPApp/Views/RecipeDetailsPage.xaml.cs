using ChefsBook_UWP_App.ViewModels;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ChefsBook_UWP_App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecipeDetailsPage : Page
    {
        public RecipeDetailsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var recipeDetailsVM = this.DataContext as RecipeDetailsViewModel;

            if (e.Parameter == null)
                return;

            var selectedRecipeId = (Guid)e.Parameter;
            recipeDetailsVM.GetRecipeDetails(selectedRecipeId);
        }
    }
}
