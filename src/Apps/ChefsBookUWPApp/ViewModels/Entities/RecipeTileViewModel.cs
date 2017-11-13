using ChefsBook.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeTileViewModel : RecipeBaseViewModel
    {
        public RecipeTileViewModel(RecipeDTO model = null) : base(model)
        {
            Model = model ?? Model;

            TagsListing = string.Join(", ", Model.Tags.Select(t => t.Name));
        }

        private RecipeDTO Model { get; set; } = new RecipeDetailsDTO {
            Tags = new List<TagDTO>(), Ingredients = new List<IngredientDTO>(), Steps = new List<StepDTO>()
        };

        private string _tagsListing;
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
