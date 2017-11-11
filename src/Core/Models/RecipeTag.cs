using System;

namespace ChefsBook.Core.Models
{
    public class RecipeTag
    {
        public Guid TagId { get; private set; }
        public Recipe Recipe { get; private set; }
        public Guid RecipeId { get; private set; }

        private RecipeTag()
        { }

        public static RecipeTag CreateFor(Recipe recipe, Guid tagId)
        {
            return new RecipeTag
            {
                Recipe = recipe,
                RecipeId = recipe.Id,
                TagId = tagId
            };
        }
    }
}
