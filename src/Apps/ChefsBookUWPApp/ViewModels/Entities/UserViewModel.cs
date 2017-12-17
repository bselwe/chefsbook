using ChefsBook.Auth.Contracts;
using GalaSoft.MvvmLight;

namespace ChefsBook_UWP_App.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        public UserViewModel(UserInfoDTO model = null, string accessToken = "")
        {
            Model = model ?? new UserInfoDTO();
            AccessToken = accessToken;
        }

        public static explicit operator UserInfoDTO(UserViewModel viewModel)
        {
            return viewModel.Model;
        }

        private UserInfoDTO Model { get; set; }

        public string Name
        {
            get => $"{Model.FirstName} {Model.LastName}";
        }

        public string Email
        {
            get => Model.Email;
        }

        private string _accessToken = string.Empty;
        public string AccessToken
        {
            get => _accessToken;
            set => Set(ref _accessToken, value);
        }
    }
}
