using System;

namespace ChefsBook.Core.Models
{
    public class Ingredient
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Quantity { get; private set; }
        public int SequenceNumber { get; private set; }

        private Ingredient() { }

        public static Ingredient Create(string name, string quantity, int sequenceNumber = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Ingredient name cannot be empty or whitespace.");
            }

            return new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = name,
                Quantity = quantity,
                SequenceNumber = sequenceNumber
            };
        }
    }
}