using System;
using System.Collections.Generic;

namespace ChefsBook.Core.Models
{
    public class Recipe
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public TimeSpan? Duration { get; private set; }
        public int? Servings { get; private set; }
        public string Notes { get; private set; }
        public IList<Ingredient> Ingredients { get; private set; }
        public IList<Step> Steps { get; private set; }
        public IList<Tag> Tags { get; private set; }

        public static Recipe Create(string title, string description, TimeSpan? duration, int? servings, string notes)
        {
            return new Recipe
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                Duration = duration,
                Servings = servings,
                Notes = notes
            };
        }
    }
}