using System;
using System.Collections.Generic;

namespace ChefsBook.Core.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public TimeSpan Duration { get; set; }
        public int Servings { get; set; }
        public string Notes { get; set; }
        public IList<Ingredient> Ingredients { get; set; }
        public IList<Step> Steps { get; set; }
        public IList<Tag> Tags { get; set; }
    }
}