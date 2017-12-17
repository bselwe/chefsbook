using GalaSoft.MvvmLight;

namespace ChefsBook_UWP_App.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        private string _accessToken = string.Empty;
        public string AccessToken
        {
            get => _accessToken;
            set => Set(ref _accessToken, value);
        }
    }
}
