using System;
using System.Collections.Generic;

namespace ChefsBook.Core.Models
{
    public class Tag
    {
        private readonly List<RecipeTag> recipes = new List<RecipeTag>();

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyList<RecipeTag> Recipes => recipes;

        private Tag() { }

        public static Tag Create(string name)
        {
            return new Tag
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        public static Tag Create(Guid id, string name)
        {
            return new Tag
            {
                Id = id,
                Name = name
            };
        }
    }
}