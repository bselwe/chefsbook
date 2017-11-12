using ChefsBook.Core.Contracts;

namespace ChefsBook_UWP_App.ViewModels
{
    public class RecipeTileViewModel : RecipeBaseViewModel
    {
        public RecipeTileViewModel(RecipeDTO model = null) : base(model)
        {
        }

        private RecipeDTO Model { get; set; }
    }
}
