using ChefsBook.Core.Models;
using GalaSoft.MvvmLight;
using System;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeViewModel : ViewModelBase
    {
        public RecipeViewModel(Recipe model = null)
        {
            Model = model ?? new Recipe();
        }

        internal Recipe Model { get; set; }

        public Guid Id
        {
            get => Model.Id;
            set
            {
                if (value != Model.Id)
                {
                    Model.Id = value;
                    RaisePropertyChanged(() => Id);
                }
            }
        }

        public string Name
        {
            get => Model.Name;
            set
            {
                if(value != Model.Name)
                {
                    Model.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }
    }
}
