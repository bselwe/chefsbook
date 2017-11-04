using System;
using System.Collections.Generic;

namespace WebApiApp.Models
{
    public class NewRecipeParams
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? Servings { get; set; }
        public string Notes { get; set; }
        public List<RecipeIngredientParams> Ingredients { get; set; }
        public List<RecipeStepsParams> Steps { get; set; }
    }

    public class RecipeIngredientParams
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
    }

    public class RecipeStepsParams
    {
        public TimeSpan? Duration { get; set; }
        public string Description { get; set; }
    }
}