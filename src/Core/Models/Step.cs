using System;

namespace ChefsBook.Core.Models
{
    public class Step
    {
        public Guid StepId { get; set; }
        public Recipe Recipe { get; set; }
        public Guid RecipeId { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
    }
}