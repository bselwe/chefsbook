using ChefsBook.Core.Contracts;
using GalaSoft.MvvmLight;

namespace ChefsBook_UWP_App.ViewModels
{
    public class IngredientViewModel : ViewModelBase
    {
        public IngredientViewModel(IngredientDTO model = null)
        {
            Model = model ?? new IngredientDTO();
        }

        public static explicit operator IngredientDTO(IngredientViewModel viewModel)
        {
            return viewModel.Model;
        }

        private IngredientDTO Model { get; set; }

        public string Name
        {
            get => Model.Name;
            set
            {
                if (value != Model.Name)
                {
                    Model.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        public string Quantity
        {
            get => Model.Quantity;
            set
            {
                if (value != Model.Quantity)
                {
                    Model.Quantity = value;
                    RaisePropertyChanged(() => Quantity);
                }
            }
        }
    }
}
