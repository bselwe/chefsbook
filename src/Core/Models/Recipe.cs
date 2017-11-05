using System;
using System.Collections.Generic;

namespace ChefsBook.Core.Models
{
    public class Recipe
    {
        private readonly List<Ingredient> ingredients = new List<Ingredient>();
        private readonly List<Step> steps = new List<Step>();
        private readonly List<Tag> tags = new List<Tag>();

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public TimeSpan? Duration { get; private set; }
        public int? Servings { get; private set; }
        public string Notes { get; private set; }
        public IReadOnlyList<Ingredient> Ingredients => ingredients;
        public IReadOnlyList<Step> Steps => steps;
        public IReadOnlyList<Tag> Tags => tags;

        private Recipe() { }

        public static Recipe Create(string title, string description, TimeSpan? duration, int? servings, string notes)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty or whitespace.");
            }

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

        public void Update(string title, string description, TimeSpan? duration, int? servings, string notes)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty or whitespace.");
            }

            Title = title;
            Description = description;
            Duration = duration;
            Servings = servings;
            Notes = notes;
        }

        public void AddIngredients(IList<Ingredient> ingredients)
        {
            this.ingredients.AddRange(ingredients);
        }

        public void AddSteps(IList<Step> steps)
        {
            this.steps.AddRange(steps);
        }

        public void UpdateIngredients(IList<Ingredient> ingredients)
        {
            this.ingredients.Clear();
            this.ingredients.AddRange(ingredients);
        }

        public void UpdateSteps(IList<Step> steps)
        {
            this.steps.Clear();
            this.steps.AddRange(steps);
        }
    }
}