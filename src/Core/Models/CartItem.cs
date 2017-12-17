using System;

namespace ChefsBook.Core.Models
{
    public class CartItem
    {
        public Guid UserId { get; private set; }
        public Guid RecipeId { get; private set; }

        private CartItem() { }

        public static CartItem Create(Guid userId, Guid recipeId)
        {
            return new CartItem()
            {
                UserId = userId,
                RecipeId = recipeId
            };
        }
    }
}