using System;
using System.Collections.Generic;

namespace ChefsBook.Core.Models
{
    public class Recipe
    {
        private readonly List<Ingredient> ingredients = new List<Ingredient>();
        private readonly List<Step> steps = new List<Step>();
        private readonly List<RecipeTag> tags = new List<RecipeTag>();

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public TimeSpan? Duration { get; private set; }
        public int? Servings { get; private set; }
        public string Notes { get; private set; }
        public IReadOnlyList<Ingredient> Ingredients => ingredients;
        public IReadOnlyList<Step> Steps => steps;
        public IReadOnlyList<RecipeTag> Tags => tags;

        private Recipe() { }

        public static Recipe Create(
            string title, string description, TimeSpan? duration, int? servings, string notes, 
            IList<Ingredient> ingredients, IList<Step> steps)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty or whitespace.");
            }

            var recipe = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                Duration = duration,
                Servings = servings,
                Notes = notes
            };

            recipe.ingredients.AddRange(ingredients);
            recipe.steps.AddRange(steps);

            return recipe;
        }

        public void Update(
            string title, string description, TimeSpan? duration, int? servings, string notes,
            IList<Ingredient> ingredients, IList<Step> steps)
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
            
            this.ingredients.Clear();
            this.steps.Clear();

            this.ingredients.AddRange(ingredients);
            this.steps.AddRange(steps);
        }

        public void AddTags(IList<RecipeTag> tags)
        {
            this.tags.AddRange(tags);
        }

        public void UpdateTags(IList<RecipeTag> tags)
        {
            this.tags.Clear();
            this.tags.AddRange(tags);
        }

        public void RemoveTags()
        {
            this.tags.Clear();
        }
    }
}