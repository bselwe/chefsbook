using ChefsBook.Core.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeDetailsViewModel : RecipeViewModel
    {
        public RecipeDetailsViewModel(RecipeDetailsDTO model = null) : base(model)
        {
            Model = model ?? Model;

            Ingredients = new ObservableCollection<IngredientViewModel>(Model.Ingredients.ConvertAll(i => new IngredientViewModel(i)));
            Steps = new ObservableCollection<StepViewModel>(Model.Steps.ConvertAll(s => new StepViewModel(s)));
        }

        public static explicit operator RecipeDetailsDTO(RecipeDetailsViewModel viewModel)
        {
            var dto = viewModel.Model;

            dto.Ingredients = viewModel.Ingredients.ToList().ConvertAll(i => (IngredientDTO)i);
            dto.Steps = viewModel.Steps.ToList().ConvertAll(s => (StepDTO)s);

            return dto;
        }

        private RecipeDetailsDTO Model { get; set; } = new RecipeDetailsDTO { Ingredients = new List<IngredientDTO>(), Steps = new List<StepDTO>() };

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
