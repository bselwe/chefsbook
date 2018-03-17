using System;

namespace ChefsBook.Core.Models
{
    public class CartItem
    {
        public Guid UserId { get; private set; }
        public Guid RecipeId { get; private set; }
        public Recipe Recipe { get; private set; }

        private CartItem() { }

        public static CartItem Create(Guid userId, Recipe recipe)
        {
            return new CartItem()
            {
                UserId = userId,
                Recipe = recipe,
                RecipeId = recipe.RecipeId
            };
        }
    }
}