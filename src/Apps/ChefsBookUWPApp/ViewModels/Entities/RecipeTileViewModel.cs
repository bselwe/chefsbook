using ChefsBook.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeTileViewModel : RecipeBaseViewModel
    {
        public RecipeTileViewModel()
        {
            base.Model = Model;
        }

        public RecipeTileViewModel(RecipeDTO model) : base(model)
        {
            Model = model;

            TagsListing = string.Join(", ", Model.Tags.Select(t => t.Name));
        }

        private new RecipeDTO Model { get; set; } = new RecipeDetailsDTO {
            Tags = new List<TagDTO>(), Ingredients = new List<IngredientDTO>(), Steps = new List<StepDTO>()
        };

        private string _tagsListing = string.Empty;
        public string TagsListing
        {
            get => _tagsListing;
            private set
            {
                if (value != _tagsListing)
                {
                    _tagsListing = value;
                    RaisePropertyChanged(() => TagsListing);
                }
            }
        }
    }
}
