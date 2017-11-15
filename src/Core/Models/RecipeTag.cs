using System;

namespace ChefsBook.Core.Models
{
    public class RecipeTag
    {
        public Tag Tag { get; private set; }
        public Guid TagId { get; private set; }
        public Guid RecipeId { get; private set; }

        private RecipeTag()
        { }

        public static RecipeTag Create(Tag tag, Guid recipeId)
        {
            if (tag == null)
            {
                throw new ArgumentException("Tag cannot be null");
            }

            return new RecipeTag
            {
                Tag = tag,
                TagId = tag.Id,
                RecipeId = recipeId,
            };
        }
    }
}
