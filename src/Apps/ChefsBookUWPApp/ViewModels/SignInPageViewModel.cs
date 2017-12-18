using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ChefsBook_UWP_App.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private const string CredentialManagerResourceKey = "ChefsBook";
        private const string RoamingUserIdKey = "CurrentUserId";

        public SignInPageViewModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        private bool _isSigningIn;
        public bool IsSigningIn
        {
            get => _isSigningIn;
            set => Set(ref _isSigningIn, value);
        }

        private RelayCommand _signInCommand;
        public RelayCommand SignInCommand
        {
            get
            {
                return _signInCommand
                    ?? (_signInCommand = new RelayCommand(SignIn));
            }
        }

        private async void SignIn()
        {
            OnSignInStarted();
            var user = await TrySignInSilentlyAsync();
            if (user == null)
            {
                user = await ExplicitSignIn();
            }

            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(AppRootPage), user);
        }

        private async Task<UserViewModel> TrySignInSilentlyAsync()
        {
            try
            {
                if (!ApplicationData.Current.RoamingSettings.Values.ContainsKey(RoamingUserIdKey))
                {
                    return null;
                }

                string userId = ApplicationData.Current.RoamingSettings.Values[RoamingUserIdKey].ToString();

                var vault = new PasswordVault();
                var credential = vault.RetrieveAll().FirstOrDefault(x => x.Resource == CredentialManagerResourceKey &&
                    x.UserName == userId);
                if (null == credential)
                {
                    return null;
                }
                credential.RetrievePassword();

                var userDto = await _apiService.SignIn(credential.Password);

                if (userDto == null)
                    return null;

                return new UserViewModel(userDto, credential.Password);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<UserViewModel> ExplicitSignIn()
        {
            var authService = new GoogleAuthService();
            var accessToken = await authService.SignIn();
            var userDto = await _apiService.SignIn(accessToken);
            var user = new UserViewModel(userDto, accessToken);

            StoreUserInfo(user);

            return user;
        }

        private void StoreUserInfo(UserViewModel user)
        {
            ApplicationData.Current.RoamingSettings.Values[RoamingUserIdKey] = user.Email;

            var vault = new PasswordVault();
            foreach (var c in vault.RetrieveAll().Where(x => x.Resource == CredentialManagerResourceKey))
            {
                vault.Remove(c);
            }

            var apiCredential = new PasswordCredential
            {
                UserName = user.Email,
                Password = user.AccessToken,
                Resource = CredentialManagerResourceKey
            };
            vault.Add(apiCredential);
        }

        private void OnSignInStarted()
        {
            IsSigningIn = true;
        }

        private void OnSignInFailed(string error = null)
        {
            IsSigningIn = false;
        }
    }
}
