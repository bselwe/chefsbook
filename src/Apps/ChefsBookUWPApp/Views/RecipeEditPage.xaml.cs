using ChefsBook_UWP_App.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ChefsBook_UWP_App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecipeEditPage : Page
    {
        private EditRecipePageViewModel ViewModel { get; set; } = ServiceLocator.Current.GetInstance<EditRecipePageViewModel>();

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

        private async void ChooseImageButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            RecipeViewModel recipe = ((sender as Control).DataContext as EditRecipePageViewModel).Recipe;

            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");

            StorageFile pictureSourceFile = await openPicker.PickSingleFileAsync();

            if (pictureSourceFile == null)
                return;

            ViewModel.SaveImagePathCommand.Execute("");

            var localImagesFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("RecipeImages", CreationCollisionOption.OpenIfExists);
            var targetFileName = recipe.Id.ToString() + pictureSourceFile.FileType;
            var savedFile = await pictureSourceFile.CopyAsync(localImagesFolder, targetFileName, NameCollisionOption.ReplaceExisting);
            ViewModel.SaveImagePathCommand.Execute(savedFile.Name);
        }
    }
}
