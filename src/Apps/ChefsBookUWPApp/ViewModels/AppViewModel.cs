using GalaSoft.MvvmLight;

namespace ChefsBook_UWP_App.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        private UserViewModel _user;
        public UserViewModel User
        {
            get => _user;
            set => Set(ref _user, value);
        }
    }
}
