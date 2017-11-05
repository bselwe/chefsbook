using System;
using System.Collections.Generic;

namespace Core.Contracts.Commands
{
    public class UpdateRecipeDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? Servings { get; set; }
        public string Notes { get; set; }
        public List<NewIngredientDTO> Ingredients { get; set; }
        public List<NewStepDTO> Steps { get; set; }
    }

    public class UpdateIngredientDTO
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
    }

    public class UpdateStepDTO
    {
        public TimeSpan? Duration { get; set; }
        public string Description { get; set; }
    }
}