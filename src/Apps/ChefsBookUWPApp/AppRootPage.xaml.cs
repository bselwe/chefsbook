using ChefsBook_UWP_App.Services.Models;
using ChefsBook_UWP_App.ViewModels;
using ChefsBook_UWP_App.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ChefsBook_UWP_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppRootPage : Page
    {
        private const string viewsNamespace = "ChefsBook_UWP_App.Views";
        private AppViewModel ViewModel { get; set; } = ServiceLocator.Current.GetInstance<AppViewModel>();

        public AppRootPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter == null)
                return;

            ViewModel.User = (UserViewModel)e.Parameter;
        }

        private void NavigationMenu_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(HomePage));
        }

        private void NavigationMenu_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (!args.IsSettingsSelected)
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;
                string pageName = viewsNamespace + "." + ((string)selectedItem.Tag);
                Type pageType = Type.GetType(pageName);
                ContentFrame.Navigate(pageType);
            }
        }

        /// <summary>
        /// Ensures the nav menu reflects reality when navigation is triggered outside of
        /// the nav menu buttons.
        /// </summary>
        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                foreach (var menuItemObject in NavigationMenu.MenuItems)
                {
                    var menuItem = menuItemObject as NavigationViewItem;

                    if (menuItem == null)
                        continue;

                    string pageName = viewsNamespace + "." + ((string)menuItem.Tag);
                    Type pageType = Type.GetType(pageName);
                    if (pageType == e.SourcePageType)
                    {
                        NavigationMenu.SelectedItem = menuItemObject;
                        break;
                    }
                }
            }
        }

        #region BackRequested Handlers

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            bool handled = e.Handled;
            BackRequested(ref handled);
            e.Handled = handled;
        }

        private void BackRequested(ref bool handled)
        {
            if (ContentFrame == null)
                return;

            if (ContentFrame.CanGoBack && !handled)
            {
                handled = true;
                ContentFrame.GoBack();
            }
        }

        #endregion
    }
}
