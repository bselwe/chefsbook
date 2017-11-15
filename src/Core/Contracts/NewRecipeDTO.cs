using System;
using System.Collections.Generic;

namespace ChefsBook.Core.Contracts
{
    public class NewRecipeDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? Servings { get; set; }
        public string Notes { get; set; }
        public List<NewRecipeIngredientDTO> Ingredients { get; set; }
        public List<NewRecipeStepDTO> Steps { get; set; }
        public List<NewRecipeTagDTO> Tags { get; set; }
    }

    public class NewRecipeIngredientDTO
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
    }

    public class NewRecipeStepDTO
    {
        public TimeSpan? Duration { get; set; }
        public string Description { get; set; }
    }

    public class NewRecipeTagDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}