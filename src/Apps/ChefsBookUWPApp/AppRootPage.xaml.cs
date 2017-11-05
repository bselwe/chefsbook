using ChefsBook_UWP_App.Views;
using System;
using System.Collections.Generic;
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

        public AppRootPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
        }

        public Frame AppFrame { get { return contentFrame; } }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            AppFrame.Navigate(typeof(HomePage));
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (!args.IsSettingsSelected)
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;
                string pageName = viewsNamespace + "." + ((string)selectedItem.Tag);
                Type pageType = Type.GetType(pageName);
                AppFrame.Navigate(pageType);
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
                foreach (var menuItemObject in navigationMenu.MenuItems)
                {
                    var menuItem = menuItemObject as NavigationViewItem;

                    if (menuItem == null)
                        continue;

                    string pageName = viewsNamespace + "." + ((string)menuItem.Tag);
                    Type pageType = Type.GetType(pageName);
                    if (pageType == e.SourcePageType)
                    {
                        navigationMenu.SelectedItem = menuItemObject;
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
            if (AppFrame == null)
                return;

            if (AppFrame.CanGoBack && !handled)
            {
                handled = true;
                AppFrame.GoBack();
            }
        }

        #endregion
    }
}
