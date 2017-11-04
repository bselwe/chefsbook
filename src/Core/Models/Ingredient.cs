using System;

namespace ChefsBook.Core.Models
{
    public class Ingredient
    {
        public Guid IngredientId { get; private set; }
        public Recipe Recipe { get; private set; }
        public Guid RecipeId { get; private set; }
        public string Name { get; private set; }
        public string Quantity { get; private set; }

        public static Ingredient Create(Recipe recipe, string name, string quantity)
        {
            return new Ingredient
            {
                IngredientId = Guid.NewGuid(),
                Recipe = recipe,
                RecipeId = recipe.Id,
                Name = name,
                Quantity = quantity
            };
        }
    }
}