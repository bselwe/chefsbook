﻿using ChefsBook.Core.Contracts;
using GalaSoft.MvvmLight;

namespace ChefsBook_UWP_App.ViewModels
{
    public class TagViewModel : ViewModelBase
    {
        public TagViewModel(TagDTO model = null)
        {
            Model = model ?? new TagDTO();
        }

        public static explicit operator TagDTO(TagViewModel viewModel)
        {
            return viewModel.Model;
        }

        private TagDTO Model { get; set; }

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
    }
}
