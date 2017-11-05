using System;
using System.Collections.Generic;

namespace Core.Contracts.Commands
{
    public class NewRecipeDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? Servings { get; set; }
        public string Notes { get; set; }
        public List<NewIngredientDTO> Ingredients { get; set; }
        public List<NewStepDTO> Steps { get; set; }
    }

    public class NewIngredientDTO
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
    }

    public class NewStepDTO
    {
        public TimeSpan? Duration { get; set; }
        public string Description { get; set; }
    }
}