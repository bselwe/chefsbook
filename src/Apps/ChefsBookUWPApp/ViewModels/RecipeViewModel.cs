using ChefsBook.Core.Contracts;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeViewModel : ViewModelBase
    {
        public RecipeViewModel(RecipeDTO model = null)
        {
            Model = model ?? new RecipeDTO();

            Ingredients = new ObservableCollection<IngredientViewModel>(model.Ingredients.ConvertAll(i => new IngredientViewModel(i)));
            Steps = new ObservableCollection<StepViewModel>(model.Steps.ConvertAll(s => new StepViewModel(s)));
        }

        public static explicit operator RecipeDTO(RecipeViewModel viewModel)
        {
            var dto = viewModel.Model;

            dto.Ingredients = viewModel.Ingredients.ToList().ConvertAll(i => (IngredientDTO)i);
            dto.Steps = viewModel.Steps.ToList().ConvertAll(s => (StepDTO)s);

            return dto;
        }

        private RecipeDTO Model { get; set; }

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
                if(value != Model.Title)
                {
                    Model.Title = value;
                    RaisePropertyChanged(() => Title);
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

        public int? Servings
        {
            get => Model.Servings;
            set
            {
                if (value != Model.Servings)
                {
                    Model.Servings = value;
                    RaisePropertyChanged(() => Servings);
                }
            }
        }

        public string Notes
        {
            get => Model.Notes;
            set
            {
                if (value != Model.Notes)
                {
                    Model.Notes = value;
                    RaisePropertyChanged(() => Notes);
                }
            }
        }

        private ObservableCollection<IngredientViewModel> _ingredients;
        public ObservableCollection<IngredientViewModel> Ingredients
        {
            get => _ingredients;
            set => Set(ref _ingredients, value);
        }

        private ObservableCollection<StepViewModel> _steps;
        public ObservableCollection<StepViewModel> Steps
        {
            get => _steps;
            set => Set(ref _steps, value);
        }
    }
}
