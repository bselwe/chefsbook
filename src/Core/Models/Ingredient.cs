using System;

namespace ChefsBook.Core.Models
{
    public class Ingredient
    {
        public Guid IngredientId { get; set; }
        public Recipe Recipe { get; set; }
        public Guid RecipeId { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
    }
}