using ChefsBook.Core.Contracts;
using GalaSoft.MvvmLight;
using System;

namespace ChefsBook_UWP_App.ViewModels
{
    public class StepViewModel : ViewModelBase
    {
        public StepViewModel(StepDTO model = null)
        {
            Model = model ?? new StepDTO();
        }

        public static explicit operator StepDTO(StepViewModel viewModel)
        {
            return viewModel.Model;
        }

        private StepDTO Model { get; set; }

        public TimeSpan? Duration
        {
            get => Model.Duration;
            set
            {
                if (value != Model.Duration)
                {
                    Model.Duration = value;
                    RaisePropertyChanged(() => Duration);
                }
            }
        }

        public string Description
        {
            get => Model.Description;
            set
            {
                if (value != Model.Description)
                {
                    Model.Description = value;
                    RaisePropertyChanged(() => Description);
                }
            }
        }

    }
}
