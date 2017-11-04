using System;

namespace ChefsBook.Core.Models
{
    public class Step
    {
        public Guid StepId { get; private set; }
        public Recipe Recipe { get; private set; }
        public Guid RecipeId { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string Description { get; private set; }

        public static Step Create(Recipe recipe, TimeSpan duration, string description)
        {
            return new Step
            {
                StepId = Guid.NewGuid(),
                Recipe = recipe,
                RecipeId = recipe.Id,
                Duration = duration,
                Description = description
            };
        }
    }
}