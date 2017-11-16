using ChefsBook.Core.Contracts;
using GalaSoft.MvvmLight;
using System;

namespace ChefsBook_UWP_App.ViewModels
{
    public abstract class RecipeBaseViewModel : ViewModelBase
    {
        public RecipeBaseViewModel(RecipeDTO model = null)
        {
            Model = model ?? Model;
        }

        protected RecipeDTO Model { get; set; }

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

        public string Title
        {
            get => Model.Title;
            set
            {
                if (value != Model.Title)
                {
                    Model.Title = value;
                    RaisePropertyChanged(() => Title);
                }
            }
        }

        public string Image
        {
            get => Model.Image;
            set
            {
                if (value != Model.Image)
                {
                    Model.Image = value;
                    RaisePropertyChanged(() => Image);
                }
            }
        }
    }
}
