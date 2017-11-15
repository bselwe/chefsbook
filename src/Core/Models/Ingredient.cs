using System;

namespace ChefsBook.Core.Models
{
    public class Ingredient
    {
        public Guid IngredientId { get; private set; }
        public string Name { get; private set; }
        public string Quantity { get; private set; }

        private Ingredient() { }

        public static Ingredient Create(string name, string quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Ingredient name cannot be empty or whitespace.");
            }

            return new Ingredient
            {
                IngredientId = Guid.NewGuid(),
                Name = name,
                Quantity = quantity
            };
        }
    }
}