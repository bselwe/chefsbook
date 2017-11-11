using System;
using System.Collections.Generic;

namespace Core.Contracts
{
    public class UpdateRecipeDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? Servings { get; set; }
        public string Notes { get; set; }
        public List<UpdateRecipeIngredientDTO> Ingredients { get; set; }
        public List<UpdateRecipeStepDTO> Steps { get; set; }
        public List<UpdateRecipeTagDTO> Tags { get; set; }
    }

    public class UpdateRecipeIngredientDTO
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
    }

    public class UpdateRecipeStepDTO
    {
        public TimeSpan? Duration { get; set; }
        public string Description { get; set; }
    }

    public class UpdateRecipeTagDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}