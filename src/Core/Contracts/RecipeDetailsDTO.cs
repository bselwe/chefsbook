using System;
using System.Collections.Generic;

namespace ChefsBook.Core.Contracts
{
    public class RecipeDetailsDTO : RecipeDTO
    {
        public List<IngredientDTO> Ingredients { get; set; }
        public List<StepDTO> Steps { get; set; }
        public List<RecipeTagDTO> Tags { get; set; }
    }
}