using ChefsBook.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeDetailsViewModel : RecipeBaseViewModel
    {
        public RecipeDetailsViewModel(RecipeDetailsDTO model = null) : base(model)
        {
            Model = model ?? Model;

            Tags = new ObservableCollection<TagViewModel>(Model.Tags.ConvertAll(t => new TagViewModel(t)));
            Ingredients = new ObservableCollection<IngredientViewModel>(Model.Ingredients.ConvertAll(i => new IngredientViewModel(i)));
            Steps = new ObservableCollection<StepViewModel>(Model.Steps.ConvertAll(s => new StepViewModel(s)));
        }

        public static explicit operator RecipeDetailsDTO(RecipeDetailsViewModel viewModel)
        {
            var dto = viewModel.Model;

            dto.Tags = viewModel.Tags.ToList().ConvertAll(t => (TagDTO)t);
            dto.Ingredients = viewModel.Ingredients.ToList().ConvertAll(i => (IngredientDTO)i);
            dto.Steps = viewModel.Steps.ToList().ConvertAll(s => (StepDTO)s);

            return dto;
        }

        private RecipeDetailsDTO Model { get; set; } = new RecipeDetailsDTO {
            Tags = new List<TagDTO>(), Ingredients = new List<IngredientDTO>(), Steps = new List<StepDTO>()
        };

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

        private ObservableCollection<TagViewModel> _tags;
        public ObservableCollection<TagViewModel> Tags
        {
            get => _tags;
            set => Set(ref _tags, value);
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
